﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DataModel;

//TODO - Deal with Telonics format with extra commas on header line, and on 3rd line
//TODO - ask to create necessary database records to support file - i.e. collars/animals/deployments
//TODO - set collarComboBox.selectedItem to null if disabled.  trick is setting back to something meaningful when enabled.
//TODO - improve validation before create/save
//TODO - validate file format
//TODO - Check to see if this file has been uploaded before (allowed but confusing) - could be slow.
//TODO - provide a progress indicator for large files
//TODO - provide better error messages when uploading invalid files

/*
 * The collar list displays the following:
 * if all is selected,  all collars (regardless of owner) for the chosen manufacturer are shown.
 * otherwise, only those collars owned by the PI of the selected project and from the chosen manufacturer are shown
 */

namespace AnimalMovement
{
    internal partial class AddFileForm : BaseForm
    {
        private AnimalMovementDataContext Database { get; set; }
        private string CurrentUser { get; set; }
        private string ProjectId { get; set; }
        private Project Project { get; set; }
        private List<Collar> AllCollars { get; set; } 
        internal event EventHandler DatabaseChanged;

        internal AddFileForm(string user)
        {
            CurrentUser = user;
            SetupForm();
        }

        internal AddFileForm(string projectId, string user)
        {
            ProjectId = projectId;
            CurrentUser = user;
            SetupForm();
        }

        private void SetupForm()
        {
            InitializeComponent();
            RestoreWindow();
            LoadDataContext();
            EnableUpload();
        }

        private void LoadDataContext()
        {
            if (ProjectId == null)
                ProjectId = Settings.GetDefaultProject();

            Database = new AnimalMovementDataContext();
            //Weirdness: Project points into our datacontext, which gets changed
            //when we requery the projects table, so setting it here doen't work
            //Database.Projects.FirstOrDefault(p => p.ProjectId == ProjectId);

            if (Database == null || CurrentUser == null)
            {
                MessageBox.Show("Insufficent information to initialize form.", "Form Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }
            //Database.Log = Console.Out;
            AllCollars = Database.Collars.ToList();
            var query = from p in Database.Projects
                        where p.ProjectInvestigator == CurrentUser ||
                              p.ProjectEditors.Any(u => u.Editor == CurrentUser)
                        select p;
            var myProjects = query.ToList();
            ProjectComboBox.DataSource = myProjects;
            Project = ProjectId == null
                    ? myProjects.FirstOrDefault()
                    : Project = myProjects.FirstOrDefault(p => p.ProjectId == ProjectId);
            ProjectComboBox.DisplayMember = "ProjectName";
            ProjectComboBox.SelectedItem = Project;
            //CollarComboBox.DataSource will be set to a filtered list once the manufacturer is selected;
            CollarComboBox.DisplayMember = "CollarId";
            CollarMfgrComboBox.DataSource = Database.LookupCollarManufacturers;
            CollarMfgrComboBox.DisplayMember = "Name";
            SetUpFormatList();
        }

        private void SetUpFormatList()
        {
            char? formatCode = Settings.GetDefaultFileFormat();
            var query = Database.LookupCollarFileFormats;
            var formats = query.ToList();
            FormatComboBox.DataSource = formats;
            FormatComboBox.DisplayMember = "Name";
            if (!formatCode.HasValue)
                return;
            var format = formats.FirstOrDefault(f => f.Code == formatCode.Value);
            if (format != null)
                FormatComboBox.SelectedItem = format;
        }

        private void EnableUpload()
        {
            UploadButton.Enabled = Project != null && !string.IsNullOrEmpty(FileNameTextBox.Text) && FormatComboBox.SelectedItem != null;
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            UploadButton.Text = "Working...";
            UploadButton.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();

            byte[] data;
            try
            {
                data = System.IO.File.ReadAllBytes(FileNameTextBox.Text);
            }
            catch (Exception ex)
            {
                string msg = "The file cannot be read.\nSystem Message:\n"+ex.Message;
                MessageBox.Show(msg, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FileNameTextBox.Focus();
                return;
            }
            var file = new CollarFile
                {
                    Project1 = Project,
                    FileName = System.IO.Path.GetFileNameWithoutExtension(FileNameTextBox.Text),
                    LookupCollarFileFormat = (LookupCollarFileFormat)FormatComboBox.SelectedItem,
                    LookupCollarManufacturer = (LookupCollarManufacturer)CollarMfgrComboBox.SelectedItem,
                    CollarId = CollarComboBox.Enabled ? ((Collar)CollarComboBox.SelectedItem).CollarId : null,
                    LookupCollarFileStatus = Database.LookupCollarFileStatus.FirstOrDefault(s => s.Code == (StatusActiveRadioButton.Checked ? 'A' : 'I')),
                    Contents = data
                };
            Database.CollarFiles.InsertOnSubmit(file);

            try
            {
                Database.SubmitChanges();
            }
            catch (Exception ex)
            {
                Database.CollarFiles.DeleteOnSubmit(file);
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FileNameTextBox.Focus();
                UploadButton.Text = "Upload";
                Cursor.Current = Cursors.Default;
                return;
            }
            Cursor.Current = Cursors.Default;

            OnDatabaseChanged();
            UploadButton.Text = "Upload";
            FileNameTextBox.Text = String.Empty;
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileNameTextBox.Text = openFileDialog.FileName;
                LookupCollarFileFormat format = GuessFileFormat(FileNameTextBox.Text);
                FormatComboBox.SelectedItem = format;
            }
        }

        private LookupCollarFileFormat GuessFileFormat(string path)
        {
            string fileHeader = System.IO.File.ReadLines(path).First().Trim();
            var db = new SettingsDataContext();
            var header = db.LookupCollarFileHeaders.FirstOrDefault(h => h.Header == fileHeader);
            char code = header != null ? header.FileFormat : '?';
            return Database.LookupCollarFileFormats.FirstOrDefault(f => f.Code == code);
        }

        private void FileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            EnableUpload();
        }

        private void ProjectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Project = ProjectComboBox.SelectedItem as Project;
            if (Project != null)
                Settings.SetDefaultProject(Project.ProjectId);
            EnableUpload();
            RefreshCollarComboBox();
        }

        private void FormatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormatComboBox.SelectedItem != null)
            {
                var format = (LookupCollarFileFormat)FormatComboBox.SelectedItem;
                CollarComboBox.Enabled = format.HasCollarIdColumn == 'N';
                CollarMfgrComboBox.SelectedItem = format.LookupCollarManufacturer;
                Settings.SetDefaultFileFormat(format.Code);
            }
            EnableUpload();
        }

        private void CollarMfgrComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshCollarComboBox();
        }

        private void AllCollarsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCollarComboBox();
        }

        private void RefreshCollarComboBox()
        {
            IEnumerable<Collar> data = AllCollars;
            if (CollarMfgrComboBox.SelectedItem != null)
                data = data.Where(
                c => c.CollarManufacturer == ((LookupCollarManufacturer)CollarMfgrComboBox.SelectedItem).CollarManufacturer);
            if (!AllCollarsCheckBox.Checked && Project != null)
                data = data.Where(c => c.Manager.Equals(Project.ProjectInvestigator, StringComparison.InvariantCultureIgnoreCase));
            CollarComboBox.DataSource = data.ToList();
        }

        private void OnDatabaseChanged()
        {
            EventHandler handle = DatabaseChanged;
            if (handle != null)
                handle(this,EventArgs.Empty);
        }

    }
}
