# Database Tables

The tables were documented in a very ad hoc way, and the format and amount of
documentation varies greatly by table. Sorry. I intend to document all the
table per the following example table.

# Example

## Description

What objects does this table contain, and what is the purpose of these objects?

## Relationships

What objects "own" this object? What are this objects children?

## Attributes (Columns)

How do we describe these objects, and what are valid values.

## Select

Who can view these objects.  Describe any row or column level restrictions

## Create

Who can create these objects. If they are created by the system, then include a
complete list of the tables, attributes, and values it is derived from.  This is
needed to ensure that changes to those tables, attributes, and values trigger
appropriate changes to these objects.

## Update

Who can update these objects. Describe any row or column level restrictions.
If they are updated by the system, then include a complete list of the
influencing table, attributes, and values.

## Delete

Who can delete these objects. Describe any row level restrictions. If they are
updated by the system, then include a complete list of the influencing
tables, attributes, and values.


#
# CollarParameters

## Description

  This table contains information on how to process a Collar Files. Each record
  contains a Collar, and either a link to a Collar Parameter File, or a time span.
  This table is currently only useful for Telonics collars with Argos data. Other
  arrangements that require more than a parameter file (of any format) to process
  a Collar File will require modifications to this table.  A Collar can have only
  one Collar Parameter at a time.  The Start and End dates are used to specify the
  period during which a parameter is valid for a file.
  
## Relationships

  A Collar can have one or more Collar Parameter (at different times).  A Collar
  Parameter File can provide Collar Parameters for multiple Collars concurrently.
  Therefore, a Collar Parameter must have a Collar (foreign key), and optionally a
  Collar Parameter File (nullable foreign Key).
  Collar Files that are created by a using the Collar Parameter maintain a link to the
  Collar Parameter (foreign key), which will deny deletes of the Collar Parameter.  How
  a Collar File was derived is important to keeping it up to date, so we need to maintain
  the Collar Parameters used to create it.  If we want to delete a CollarParameter, we
  must first delete the derived files that depend on it.
  
## Attributes (Columns)

* `ParameterId`
  - non-null surrogate primary key (integer starting at 1 and incrementing
    by 1).  Users cannot add or update this column.
* `CollarManufacturer/CollarId`
  - non-null foreign key to Collars  Cascade on delete and
    update. Parameter is subject to changes and deletes in Collars.  A
		CollarParameter cannot begin after a Collars disposal date, but it may continue
		to be valid after the collar is disposed (i.e. the end date is after the collar
		disposal date.
* `FileId`
  - nullable foreign key to CollarParameterFiles. Cascade on delete and No Action
    on update (FileId is a surrogate PK, it will never change).  A Collar Parameter
		cannot use an Inactive Collar Parameter File. One and only one of FileId and
		Gen3Period must be non-null.
* `Gen3Period`
  - nullable positive integer time span in minutes between GPS fixes on an
    Telonics Gen3 collar. One and only one of FileId and Gen3Period must be
		non-null. 
* `StartDate`
  - nullable date/time in UTC indicating when this Collar Parameter begins to
    apply to the collar.  A value of null indicates the infinite past.
* `EndDate`
  - nullable date/time in UTC indicating when this Collar Parameter stops
    applying to the collar.  A value of null indicates the infinite future.
  
## Select

Any database user can view any attribute of any Collar Parameter.

## Create

Only a database user who is a Project Investigator, or an Assistant to a Project
Investigator can create Collar Parameter for a collar that the PI owns.  The database
will never create these objects automatically.  Creating different Collar Parameters for
the same Collar with overlapping (touching is not overlapping) dates is not allowed.
One and only one of FileId and Gen3Period must be non-null.  The database user creating
a Collar Parameter with a Collar Parameter File must be the owner or an assistant to
the owner of the Collar Parameter File.
When a Collar Parameter is created, the FileProcessingIssues should be scanned, and
or Flag the appropriate files for reprocessing
any file issues that can be resolved with the new Collar Parameter should be resolved
by reprocessing the file.

## Update

Only the database user who owns the Collar or Collar Parameter File, or is an Assistant
to the Collar or Collar Parameter File owner can update a Collar Parameter for the
Collar or Collar Parameter File.  The following restrictions apply to changing
attributes:

- `ParameterId`
  - managed by the database, the user cannot change this attribute.  The
    database will not change it after creation.
- `CollarManufacturer/CollarId`
  - If these values changes, then all of the files generated
    with this Collar Parameter will need to be deleted.  The file processing issues should
    be scanned to see if there are issues that are resolved with the new Collar Parameter.
- `Gen3Period, FileId`
  - If these values changes, then all of the files generated with this
    Collar Parameter will need to be regenerated. 
- `StartDate`
  - Cannot overlap with another Collar Parameter for the same collar.  The
    dates (typically only a null end date) will be changed to accommodate a new Collar
    Parameter on a Collar.  When this happens, any files that were created with this
    Collar Parameter must be evaluated to ensure that the generated fixes are all
    inside the new date range, and if the date range increased, then new fixes may need
    to be generated. 

## Delete

Only the database user who owns the Collar, or is an Assistant to the Collar owner can
delete a Collar Parameter. The database will never delete these objects automatically.
Collar Parameter deletes cannot be done if the Collar Parameter was used to create a
Collar File - see the Special Note on Referential Integrity below.  Therefore, if the
client wants to delete a CollarParameter, they must first delete related CollarFiles.
Deleting a CollarFile is sufficient to list the parents file in need of reprocessing
(the parent CollarFile will be listed in ArgosPlatformDates but will not have a derived
CollarFile, and no ProcessingIssues to explain it), after a scheduled attempt to
process, the file will have a processing issue to blaming the missing CollarParameter.

## Special Note on Referential Integrity

If a CollarParameter is deleted, all CollarFiles which reference it need to be deleted.
I would prefer to do this as an 'DELETE ON CASCADE' in a foreign key relationship in the
CollarFiles table, however CollarFiles has an INSTEAD OF trigger (for reasons similar to
those discussed here, and because of it's ParentFileId self-referential foreign key
relationship). Per [MSDN](http://msdn.microsoft.com/en-us/library/ms186973(v=sql.105).aspx),
"A table that has an INSTEAD OF trigger cannot also have a REFERENCES clause that
specifies a cascading action", so CollarFiles cannot have a DELETE ON CASCADE constraint.

In addition, per [MSDN](http://msdn.microsoft.com/en-us/library/ms189261(v=sql.105).aspx),
"If constraints exist on the trigger table, they are checked after the INSTEAD OF trigger
execution but prior to the AFTER trigger execution. If the constraints are violated, the
INSTEAD OF trigger actions are rolled back and the AFTER trigger is not executed."
Therefore if CollarFiles has a DELETE NO ACTION constraint it will block the delete of
CollarParameters.  I would need to use another INSTEAD OF trigger to delete the
CollarFiles before deleting the CollarParameters. However, this would mean I cannot use
DELETE ON CASCADE in the CollarParameter's relationship to CollarParameterFiles, etc.

Three solutions
1) Implement INSTEAD OF triggers (or avoid cascades) all the way down.
2) Require the client to delete referential problems before deleting the CollarParameters
3) Remove the foreign key constraint, and use AFTER update, insert, delete triggers
   to maintain referential integrity.

I like the explicit relationships, and cascading triggers (they are simple to understand,
easy to maintain, and guarantee the semantics). Therefore, I do not like INSTEAD OF
triggers.  I am left with option #2.


#
# CollarParameterFiles

## Description

This table contains Collar Parameter Files used to process Collar Files. Each record
contains information about the Collar Parameter File, as well as the contents of the
Collar Parameter File.  Currently this is only used by Telonics Gen3 and Gen4 collars
to process Collar Files with Argos data.
  
## Relationships

Collar Parameter Files are owned by a Project Investigator.  A Collar Parameter File is
linked to a Collar with a Collar Parameter.  A Collar Parameter File can be linked to
multiple collars.
  
## Attributes (Columns)

* `FileId`
  - non-null surrogate primary key (integer starting at 1 and incrementing by 1).
    Users cannot add or update this column.
* `Owner
  - non-null foreign key to ProjectInvestigators  Cascade on delete and update.
    File is subject to changes and deletes in ProjectInvestigators
* `Format`
  - non-null foreign key to LookupCollarParameterFileFormats. Update - Cascade,
    Delete - No Action (i.e. deny delete of format if in use).  Format
		is specified by the user, and the database does not try to verify the contents
		with the format. Behavior is unspecified if the contents and the format do not
		match.  Since the database doesn't manage the format, the user can change the
		format, but only if the file is not linked to a collar.
* `Status`
  - non-null foreign key to LookupFileStatus. Update - Cascade, Delete - No Action
    (i.e. update to match changes in status code, and deny delete of format if in
		use) - Currently two status A- Active and I - Inactive.  Inactive Collar
		Parameter Files cannot be linked to a collar (and they appear different in the
		UI).  A file cannot be made Inactive if it is linked to a collar.
* `FileName`
  - non-null text name the file.  For information only.
* Contents
  - non-null binary contents of the file.  Not changeable.
* `Sha1Hash`
  - non-null computed (and cached) hash value of the file contents, for avoiding
    duplicates.  Users cannot add or update this column.
* `UploadDate`
  - non-null date time when the file was added to the database. Users cannot
    add or update this column.
* `UploadUser`
  - non-null database login name of the user who added this file to the
    database. Users cannot add or update this column.
  
## Select

Any database user can view any attribute of any Collar Parameter Files.

## Create

Only a database user who is a Project Investigator, or an Assistant to a Project
Investigator can create Collar Parameter Files for that PI.  The database will never
create these objects automatically.  Creating different Collar Parameter Files with
identical contents (and Sha1Hash) is allowed, but potentially confusing for users.

## Update

Only the database user who is the owning Project Investigator, or an Assistant to the
owning Project Investigator can update a Collar Parameter Files.  The following
restrictions apply to changing attributes:
- `FileId, Contents, Sha1Hash, UploadDate, UploadUser`: managed by the database, the user
  cannot change these attributes.  The database will not change them after creation.
- `Format, Status`: cannot be changed if the file is linked to a collar.  Note, this will
  effectively block cascading updates from LookupCollarParameterFileFormats and 
  LookupFileStatus which is OK, since those tables shouldn't change anyway.
- `Owner`: The database user making the update must be the receiving PI or an Assistant
  to the receiving PI. 
- `FileName`: no restrictions

## Delete

Only the database user who is the owning Project Investigator, or an Assistant to the
owning Project Investigator can delete a Collar Parameter Files. The database will never
delete these objects automatically.  Collar Parameter Files deletes will cascade to the
all related Collar Parameters (link between Collars and Collar Parameter Files).  A
Collar Parameter cannot be deleted if it was used to created a CollarFile.  Therefore a
Collar Parameter File cannot be deleted if it was used to create a CollarFile.


#
# CollarFiles

## Column constraints

* `CollarManufacturer/CollarId`
  - is a nullable Foreign Key to the collars table
  - Can only be null for files which will be processed, i.e will have sub files with
    non-null collars
  - It will cascade updates, but stop deletes
* `ProjectId`
  - is a nullable key in the projects table (determines where this file is visible in UI).
    It will cascade updates, but stop deletes
* `Owner`
  - is a nullable key in the PI table (determines where this file is visible in UI)
  - It will cascade updates, but stop deletes
  - one and only one of Project/Owner must be non-null. enforced by an insert trigger.
    if owner and project are both null, the insert SP will use the uploading user (caller)
	as the owner.  This will fail unless the up-loader is also a PI.
  - Owner is used when a file (Argos download or email upload) will contain data for a
    number of collars, which might be spread across multiple projects.  Owner in this
    case really reflects the owner of the file, and not necessarily the owner of the
    collars or projects involved with the file.  It is typically used for downloading
	an Argos program which is done on behalf of a program manager, and not a project.
* `FileName`
  - must be non-null and not empty string; purely informational
* `Status`
  - is a non-null key in LookupFileStatus
* `Content`
  - must be non-null and is immutable
* `FileId/Username/UploadDate/Sha1Hash`
  - are created/maintained by the DB, and cannot be inserted/updated
* `ParentFileId`
  - is a nullable key in the collar file table.
  - It is inserted by the external file processors, and cannot be changed once inserted
    (Would be nice if the database could manage this - requires processing of Argos
    files entirely and only in the database.)
  - Special case: CSV files that were generated from Gen3 *.tpf files and loaded into the
    database, might in the future load the *.tpf files, in which case the parent would
    be loaded after the child.
* `Format`
  - Is a non-null key in LookupCollarFileFormat.
  - It is computed by the database from the Contents on creation and cannot be inserted by
    the user, or changed once inserted 
* `ArgosDeploymentId`
  - is a nullable key in the ArgosDeployments table.  Update and Delete No Action
    required by after triggers, so CollarFiles must be deleted before ArgosDeployments
  - It is inserted by only the external file processors so that a results file can be
    linked back to the ArgosDeployment that was in effect when this file was created.
	  This file may need to be deleted or updated if the ArgosDeployment changes.
  - This column is not visible to clients, except on insert, where it defaults to null.
    It can be manipulated (subject to FK constraints) by the SA.
  - ArgosDeploymentId must be non-null if and only if the parent has ArgosData
  - There is no simple way to verify (on insert) that this property is correct.  If this
    property is set incorrectly by the user or the SA, then all bets are off, so only
    use the FileProcessor to insert generated files.
* `CollarParameterId`
  - is a nullable key in the CollarParameters table.  Update and Delete No Action
    required by after triggers, so CollarFiles must be deleted before CollarParameters
  - It is inserted by only the external file processors so that a results file can be
    linked back to the CollarParameter that was in effect when this file was created.
	  This file may need to be deleted or updated if the CollarParameter changes.
  - This column is not visible to clients, except on insert, where it defaults to null.
    It can be manipulated (subject to FK constraints) by the SA.
  - CollarParameterId must be non-null if and only if the parent has ArgosData
  - There is no simple way to verify (on insert) that this property is correct.  If this
    property is set incorrectly by the user or the SA, then all bets are off, so only
    use the FileProcessor to insert generated files.

If a file has a non-null ParentFileId
  then only the project, owner & status can be updated, and only to mirror changes in the
  parent it can not be deleted, except when the parent is deleted or reprocessed
  it can not be inserted, except when the parent is (re)processed

## Dependencies
* Collars: Collar
* Projects: Project
* ProjectInvestigators: Owner
* LookupFileStatus: Status
* LookupFormat: Format
* CollarFiles: ParentFileId
  
## Tables Dependent on CollarFiles

* CollarDataXXX: Contents, and Format (update: na; delete: cascade)
* CollarFile: FileId (for files with a parent) (update: na; delete: cascade - can't cascade
  in the FK constraint because this 'may cause cycles or multiple cascade paths', so we
  must do it manually
* ArgosFilePlatformDates: Contents and Format   (update: na; delete: cascade)
* CollarFixes: Status, Collar (and Contents via CollarDataXXX) (update: see below;
             delete: cascade - 'Cannot define cascaded DELETE because the table has an
			 INSTEAD OF DELETE TRIGGER defined on it'.

**implement cascading deletes on ParentFileId and in CollarDataXXX this will greatly
simplify the delete SP.**

## Insert

* Enforce column constraints
* If Argos, summarize the transmissions by platform id and date range
* If Argos, process to create/insert child files
* Insert data in file format specific tables
* If status is active, generate collar fixes from new data in format specific tables

## Delete

* we delete active/inactive files the same.
* delete child files - handled in trigger because cascade doesn't work here
* delete fixes - handled in trigger because cascade doesn't work here (deleting fixes will
   cascade to the locations, and then to the movement vectors)
* delete related records in ArgosFilePlatformDates - done with cascade
* delete related records in CollarDataXXX - done with cascade.

* files that were downloaded cannot be deleted (by business decision and enforced by
  Foreign Key constraint)
* Files that are children of other files cannot be directly deleted.

To delete a file you must be an editor of the project, or the Owner of the file
The ArgosProcessor role is also allowed to delete files on behalf of users
The uploader of the file has no special delete permission. i.e. They may have lost their
editor privileges since they uploaded the file.

## Updates (by column)

* `Status`
  - If the status changes, add/remove CollarFixes for related data in format specific
    tables.  If a parent changes, then this change must be propagated to the child
    files.  Therefore, a child's status will generally mirror the parent's, the
    exception is that a child can be set to inactive when the parent is still active.
    This is to support the situation where only some of the children of a multi-collar
    parent are "replaced" by store-on-board data.

* `Collar`
  - Only allowed when a file is inactive, since fixes for a file are dependent on
    knowing the collar of a file

* `Project`
  - No effect on processing, will appear in a different project list
  - Must ripple down to sub-files. must be null/not-null if owner is non-null/null

* `Owner`
  - No effect on processing, will appear in a different PI List
  - Must ripple down to sub-files. must be null/not-null if project is non-null/null

* `FileName`
  - Purely informational, user can change as desired.


#
# CollarDataXXX

The data in these tables are not dependent on anything beyond the contents (and format)
of the related collar file.  Since the CollarFile contents/format are immutable, this
table is also immutable. Users cannot create/delete or update records in these tables.
These records are created by the database by calling an appropriate parser when a
CollarFile is inserted into the database.  The data in these tables is related via
foreign key to the CollarFile table.  It will deny updates (the CollarFile.FileId does not
change), and will cascade deletes.  Changes to the collar file status does not change
the contents of these table, only the fixes derived from the collar data.


#
# Collars

*Comming Soon*

**Updating DisposalDate needs to update any null EndDate in a related ArgosDeployment.**


#
# ProjectInvestigatorAssistants

These are DB users in the editor role.
They have been granted permission by the PI to do anything the PI can do, except
add or delete assistants (an assistant can delete him/herself).
and add or delete projects.  They are automatically a project editor on all
of a PI's project (although they will not appear in the editor list)


#
# ProjectEditors

These are DB users in the editor role.
They can make all changes that a PI can make to a project, except
add or delete project editors (an editor can delete him/herself).
they can edit all properties of a project except the PI.


#
# CollarDeployments

CollarDeployments has Foreign keys to Collars, but it blocks cascading updates
This is because it would require permitting changing the collar in a collar deployment,
this would be fine if it only happened as a result of a collar id changing, in which
case all references to a collar would be changing simultaneously and consistently.
However changing the collar in a deployment is tricky if it doesn't start from the collar
table.  (see the Argos deployment triggers).  We therefore are prohibiting changing the
collar in a deployment.
The same goes for animals in the CollarDeployment table.

If I relied on the SPROC to limit changes to CollarDeployments, I could allow changes to
the collar columns in the table, but to protect against RI damage by the SA, I enforce
the no change column rule in the trigger.

#
# CollarFixes

All writes to this table are done by CollarFixes_Insert, which is not visible to clients
CollarFixes_Insert is only called by CollarFiles insert/update triggers.
Data inserted is based on
  1) CollarDataXXX files which are based on the parsers, and CollarFiles.Contents/Format
     which are immutable. Therefore the CollarDataXXX is also immutable.
  2) For formats A-D, the Collar Mfgr/Id and Status in CollarFiles.  The triggers in
     CollarFiles call CollarFixes_Insert/Delete as appropriate for changes in the status
	 and Collar Mfgr/Id.
**3) For formats E,F, it gets the collar Mfgr/Id from the Argos Deployment at the time of
     fix.  Changes in the ArgosDeployments table should correct all the impacted fixes.
	 It is also dependent on the Collar.HasGps attribute - This change should also be
	 propagated as necessary**

The only updates allowed to this table are to hide/de-conflict overlapping fixes.

All deletes are done by CollarFixes_Delete, which is not visible to clients
CollarFixes_Delete is only called by CollarFiles update/delete triggers.

#
# ArgosFilePlatformDates

This table is used to summarize a file without having to re-scan it.
The summary information is used to determine if a file needs to be reprocessed (i.e.
if the platform to collar or collar to parameter mappings change).

The ProgramId and PlatformId are not Foreign Keys to the ArgosPrograms and ArgosPlatforms
tables, because we may load a file that does not have a program/platform in the database
We want this table to reflect what is in the file, not what is in the database.

PlatformId must be non-null, in order to know what platform the transmissions belong to.
ProgramId is nullable.  most files will provide it, however the Debevek format does not.

**We should provide a tool for checking for platforms/programs in this table that are not
in the ArgosPlatforms or ArgosPrograms tables, and adding them if appropriate.**

The files that this table is related to are files that need processing (Argos files with
Telonics messages).  These files will have no parent, but they will (at some point) have
sub files.  These files may have multiple platforms, so the FileId is not sufficient for
a primary key, however the (FileId, PlatformId) tuple is sufficient for a primary key.

Addendum: (FileId, PlatformId) tuple is NOT sufficient for a PK.  The summarize tool for
Argos Emails sorts by programId, then PlatformId.  As it turns out, an ArgosId can be in
two different programs in the same file.  We cannot make PlatformId part of the PK,
because it is null for some file types (Debevek files).  Solution: use a artificial PK,
and create indexes on fileId, and PlatformId

This table is not dependent on anything beyond the contents (and format) of the related
collar file. Since the CollarFile contents/format are immutable, this table is also
immutable.

Only the ArgosProcessor role can add records in this table - done when a file is
processed *would be nice to do when the CollarFile is added to the database*.

Records cannot be updated or deleted. Deletes will only happen as a cascade when the
CollarFile is deleted.
Inserts may be done by AnimalMovements.exe, ArgosProcessor.exe, or ArgosDownloader.exe
which may be run on the server (with a local account) or a database domain user.  We need
to control inserts with a SP, which verifies permission.  Only the user who uploaded the
file can create the summary.  


#
# ArgosFileProcessingIssues

Issues if they happen will always happen to a CollarFile.
If the collar file is deleted the related issues are also deleted (cascade)
The platform and collar fields are nullable Foreign Keys
They are used if the issue is related to a known platform/collar in the database.  If
both the platform/collar fields are null, then the issue is related to the file in
general, and not a specific platform/collar.
The issue will be deleted/updated if the related platform/collar is deleted/updated.
*Addendum* Platform is not a FK, because when processing a program file, we may come
across a platform in the program file that is not in our database.  We want to store
the platform in the issues file, for later correction.

The data in this table is highly dependent on the state of the database at the time
a file was processed. In particular, the platform to collar and collar to parameter
mappings and date ranges of those mappings.

If a CollarFile is reprocessed, all the existing issues are cleared, and new issues
(if any) are added to this table.

Records in this table cannot be deleted directly, only via the stored procedure
ArgosFile_ClearProcessingResults.  Since the file can always be reprocessed, anyone
in the Editor or ArgosProcessor role can run this procedure.
Deletes will also happen as a cascade from deletes in related tables as discussed above.
Records cannot be updated except through Foreign Key cascades as discussed above.
Insert Permissions:
Inserts should only be done when a file is processed.  A file can be processed
by the database via ArgosProcessor.exe, or by AnimalMovements.exe, ArgosProcessor.exe,
or ArgosDownloader.exe which may be run on the server (with a local account) or by a
database domain user.
We could restrict permissions to just the user who uploaded the file, or the owner (if
one), or PI/Editors on a project (if one), but since this process should be done
automatically, when a file is inserted, it seems like it can't hurt to allow anyone
in the Editor or ArgosProcessor role to process a file, and therefore insert issues.


#
# ArgosDownloads

This table records the attempts, results, and errors associated with downloading
Argos data from the Argos server.  All records have a TimeStamp (automatically inserted)
column that records when the record was added to the database (immediately after
the download attempt finished).  The TimeStamp is the primary key to the table.
Any TimeStamp provided by the client will be ignored.
If the client and the database are in different timezone and client cares about the
client time when the insert occurred, then it is up to the client to determine the
offset and adjust the display appropriately. The records in this table represent a
snapshot in time - they are immutable and non-deletable.

Data is typically downloaded by the Argos download service/scheduled task, run by a
local account on the server (ArgosProcessor role), however it may be useful to
allow an editor to do a download when they are eager for new results.  For this reason,
we need to control access to the inserts on this table, and we will use an insert SP.
permission to run the SP will be limited to the editor and ArgosProcessor roles.
No special permission check will be done for the insert, basically any editor
can log a download attempt, however the user will need to have permissions to upload
the results file or else an error may occur.  Hopefully the client will not try to
add a record to this table until after the results file has been uploaded.  The
Upload API that all client apps should use will guarantee this.

The PlatformId/ProgramId columns are nullable columns that records the Argos
Platform or Program that was attempted to be downloaded. One and only one will be
non-null.  Typically, records will only be inserted in this table by the Argos-
Downloader application which will only try to download platforms or programs in the
database tables at the time it is run.  Therefore, these columns
can be used to join to other tables that have an Argos platform or program,
but it is not a Foreign Key relation to these tables. This table represents what was
actually tried.  If a bogus platform/program was attempted this table
should not be corrected on a cascading update/delete if a PlatformId/ProgramId in the
"related" table is updated or deleted.

*We should provide a tool for checking for platforms/programs in this table that are not
in the ArgosPlatforms or ArgosPrograms tables, and adding them if appropriate.

This table will be consulted by the ArgosDownloader to determine the number of days
since the last successful download of a platform/program.  There is slight risk here
that a user may manually enter a bogus record in this table which could screw up the
download calculations, but lets trust our editors.

When a download is performed, the number of days requested is passed to the server.
this information is optionally collected in the days column.  This information can
be used to check if there were any gaps in time that might have been missed.

The ErrorMessage nullable varchar(max) and FileId nullable int represent the results.
They are mutually exclusive, and one and only one must be non-null.  We either get a file
or we get an error explaining why.  We do not allow the case, where we get a file, but
also want to record some error or observation during obtaining it.  We either succeed
or we fail.

The FileId is a nullable Foreign Key to the CollarFile table.  Because The downloads
occurred as a matter of historical fact, I have decided to not allow the deletion (i.e
forgetting or erasing the memory) of this event.  This means that the files associated
with this event cannot be deleted (they can be made inactive).  This seems appropriate,
as the files are generally small, and the data that is downloaded from the server
expires after 10 days, and cannot be retrieved at some later date.

## Summary

* `PlatformId` non null varchar(8), no FK relationship to ArgosPlatforms
* `ProgramId` non null varchar(8), no FK relationship to ArgosPlatforms
              must be null if PlatformId is non-null and visa-versa
* `TimeStamp` non-null datetime2(7) PK - default server getdate()
* `Days` null int - informational, days of data requested in download
* `ErrorMessage` null varchar(max) - error message string returned by download API
                 must be null if FileId is non-null and visa-versa
* `FileId` null int Foreign Key to CollarFiles, Cascade Update/Delete NO ACTION (deny)

An insert trigger is  used to maintain the null/not-null requirement for
ErrorMessage/FileId and also the null/not-null requirement for PlatformId/program Ids,
This maintains data integrity if an elevated account inserts directly into the table.
the insert SP, and a default constraint enforce the TimeStamp requirements.  There is
no way for a non-sa account to insert the timestamp.


#
# ArgosPrograms

## `ProgramId`

The ProgramId is used as a PK in this table, a FK in ArgosPlatforms, and a non-FK in
ArgosDownloads and ArgosFilePlatformDates as discussed in those tables.  Ideally, the
ProgramId should not change, but we may need to correct a mistake on input.  This can be
allowed by cascading updates without a problem.  We do not want to cascade updates to
ArgosDownloads or ArgosFilePlatformDates, because those table records what we attempted,
or what is in the file, not what was/is in the ArgosPrograms table. We will not cascade
deletes.  This will protect against a huge irreversible change. The user must delete all
ArgosPlatforms in an ArgosProgram before an ArgosProgram can be deleted.

## `Manager`

This the project investigator that manages this ArgosProgram.  It is a Foreign Key to
the ProjectInvestigator table with cascade on update, and no action on delete (i.e you
cannot delete the PI if they own an ArgosProgram).  Only the current PI can edit the
fields in the ArgosProgram, including re-assigning this ArgosProgram to another PI.  

## `UserName/Password`

I assume the UserName is an immutable part of the ArgosProgram (on the Argos end). The
UserName/Password combination is used to authenticate on the Argos server for downloads,
so these values need to be correct or edited as necessary to keep them current with the
settings on the Argos server.  When our API attempts a download it will user the current
values in the database.  If these fields change at some point in the future, then all we
lose is the values that were used when a previous download was attempted - thats OK.
Therefore these are mutable without restriction.

Note, that an ArgosDownload will reference an ArgosProgram either directly, or through
an ArgosPlatform.  When it does an old download will link to the current UserName and
Password, not the one that was used when the download was attempted.

**We should hide the password from all but the manager of the ArgosProgram.  However, 
anyone the can download on behalf of the manager will need to obtain and transmit the
password in clear text.  What is the best way for the database manage this?**

## `ProgramName, StartDate, EndDate, Notes`
These fields are purely informational.

## `Active`
This is mutable value that indicates if the ArgosProgram should be downloaded. If an
ArgosProgram is Active it will be downloaded in it entirety.  If it is not Active, it will
not be downloaded, nor will any ArgosPlatforms in the ArgosProgram.  If the Active bit is
null, then the ArgosProgram is deferring to the Active bit on each ArgosPlatform in the
ArgosProgram.

ArgosPlatforms are dependent on the related ArgosProgram. The download API uses the
ArgosProgram to authenticate a download for an ArgosPlatform.

An ArgosProgram can be deleted but only by the Manager, and only if there are no
related ArgosPlatforms (part of FK specification)
An ArgosProgram can be edited within the constraints above, but only by the Manager.
An ArgosProgram can only be created by a ProjectInvestigator.  A PI can create an
ArgosProgram for another PI.

#
# ArgosPlatforms

This is a list of all the ArgosPlatforms that are (or were) in an ArgosProgram. An
ArgosPlatform deployed in the field can communicate with the Argos satellite network.
Data the ArgosPlatform has is regularly made available to the Manager of the ArgosPlatform
(via email, or web site download).  Using an ArgosPlatform allows the Manager to get data
like Collar locations on a regular basis without going out into the field. 
The Manager is charged for all the platforms in a program, so the Manager may request that
Argos remove a platform from a program.  We need all relevant ArgosPlatforms in the
database to process (or reprocess) historical data files, so we will not generally not
delete an ArgosPlatform unless a mistake was made in data entry.  See the discussion on
Active and DisposalDate for handling ArgosPlatforms that are removed from an ArgosProgram.

## `PlatformId`

PlatformId is the PK, and a FK in the ArgosDeployments and ArgosFileProcessingIssues
table, and a non-FK in ArgosDownloads and ArgosFilePlatformDates as discussed in those
tables.  Ideally, the PlatformId should not change, but we may need to correct a mistake
typo made during input.  This can be allowed by cascading updates, without a problem.
We do not want to cascade updates to ArgosDownloads or ArgosFilePlatformDates as discussed
in the description of those tables. We will cascade deletes to ArgosFileProcessingIssues,
but not to ArgosDeployments.  If the user wants to delete an ArgosPlatform, they must
first review and delete all related ArgosDeployments.

## `ProgramId`

ProgramId is the Foreign Key to the related ArgosProgram.  The UserName/Password in the
related ArgosProgram is used to authenticate with the Argos web services when the user
wants to download an ArgosPlatform.  The related ArgosProgram is also used to establish
ownership (via the Manager), and for hierarchical display in the UI.  The Foreign Key   
has no action on delete to stop a delete in ArgosPrograms if it has an ArgosPlatform.
The Foreign Key has cascade on update, to stay current with a rename of an ProgramId.

## `Active`

This is mutable value that indicates if the ArgosPlatform should be downloaded in the
future.  Active has nothing to do with whether this ArgosPlatform is or can be deployed.
The Active setting for an ArgosPlatform is only consulted if the Active bit on the
related ArgosProgram is null.  New ArgosPlatform are Active by default.

## `DisposalDate`

The DisposalDate is used to reflect the status of the ArgosPlatform in the ArgosProgram as
discussed above.  A null DisposalDate indicates that the ArgosPlatform is still in the
ArgosProgram.  DisposalDate is used in the UI to change the display of the ArgosPlatform,
and filter the list of ArgosPlatforms.  The UI should not stop a user from creating a
historical deployment on an ArgosPlatform with a non null DisposalDate.

Setting the DisposalDate to a non-null value will set any null EndDate of any related
ArgosDeployment to the same value.  Changing an ArgosDeployment.EndDate will do its own
validation which may end this transaction with an error.  The DisposalDate cannot be set
to a date that conflicts with any ArgosDeployment (that is the DisposalDate must be after
the EndDate of all related ArgosDeployments).

Setting the DisposalDate will also set the ArgosPlatform.Active to false if the date is
in the past.  If the DisposalDate is in the future (not likely), then a ArgosPlatform may
remain Active after it's DisposalDate. **We should provide a tool to check for Active
ArgosPlatforms with a DisposalDate in the past.**

The DisposalDate can be cleared (set to null from non-null) if the PI recovers and/or
re-actives this ArgosPlatform.  This will not change any deployment dates or make the
ArgosPlatform Active - The user or client software will need to do this depending on
the wishes of the user.

New ArgosPlatform have a null DisposalDate by default.

When an ArgosDeployment is created or altered, it needs to validate against the
DisposalDate of the related ArgosPlatform.

## `Notes`

This fields are purely informational.

All data manipulation and permission checks occur in stored procedures

## Delete

Can only be done by the Manager of the related ArgosProgram. By the FK constraints, the
delete will fail if there are related ArgosDeployments, but it will delete all related
ArgosFileProcessingIssues. **The UI should warn the user that deleting an ArgosPlatform
that has a related record in ArgosFilePlatformDates, will preclude processing of the
related files. Since there are no related ArgosDeployments, there are no results files**
**May want to deny a delete if an ArgosPlatform is listed in an Active TPF type
CollarParameterFile**

## Update
* `PlatformId`
  - Changes are allowed, and will cascade to only ArgosDeployments and
    ArgosFileProcessingIssues as discussed above.  Changing an ArgosDeployment may result
    in a validation error that cancels the transaction.  Changing an ArgosDeployment may
    also require reprocessing files.  See ArgosDeployments for more discussion.  **The UI
    should warn the user that changing a PlatformId that is in ArgosFilePlatformDates,
    will preclude (re)processing of the related files.**
* `ProgramId`
  - Changes are allowed, and usually initiate from changes in the ArgosProgram.  Moving a
	  ArgosPlatform from one ArgosProgram to another is valid to correct for input errors.
	  **Need to provide some way to validate against the information at the Argos Server.**
* `Active, Notes`
  - No restrictions.  See discussion above.
* `DisposalDate`
  - Restrictions and cascading changes are discussed above, and will be handled in a
	  trigger to protect data integrity against edits made without the SP (typically SA) 

## Insert

Only Investigators can run this SP.  A non-null PlatformId and ProgramId is required.
Only the Manager of the related ArgosProgram can do the insert.  Since a new
ArgosPlatform has no ArgosDeployment, any DisposalDate value is valid.  Active defaults
to true.


#
# ArgosDeployments

A Manager of an ArgosPlatform can deploy it on a Collar they also Manage allowing them to
get Collar data without going in the field.  The ArgosDeployments table tracks when an
ArgosPlatform is deployed on a Collar.  The Manager cannot do the physical deployment,
that is typically done by the CollarManufacturer.  Over the lifetime of an ArgosPlatform
it may be deployed on several Collars.  Over the lifetime of a Collar, it may carry
several different ArgosPlatforms.  However, a Collar can have only one Argos Platform at
any given time, and an ArgosPlatform can only be deployed on one Collar at any given time.

## `DeploymentId`

The DeploymentId is a surrogate primary key (INT IDENTITY(1,1) NOT NULL) used to help
identify records in updates and deletes.

## `PlatformId`

PlatformId is the Foreign Key to the ArgosPlatforms table. It represents the ArgosPlatform
that is deployed on a Collar. The FK will reject (no action) a delete of the primary table
so the Manager must delete all related ArgosDeployments before deleting an ArgosPlatform.
The FK will accept (cascade) an update of the PlatformId in the ArgosPlatforms table.
Changing the PlatformId of an ArgosDeployment will invalidate the Collar value of any
CollarFiles created from processing CollarFiles containing transmission from the previous
PlatformId during the relevant time period.  Since there is now no Collar associated
with the old PlatformId (collar cannot have two platforms at the same time), the result
files for the Collar covering the relevant date range should be deleted.

## `CollarManufacturer, CollarId`

This is the Foreign Key for a Collar in the Collars table.  It represents the collar that
the ArgosPlatform is deployed on.  The FK will reject (no action) a delete of the primary
table so the Manager must delete all related deployments before deleting an ArgosPlatform.
The FK will accept (cascade) an update of the PlatformId in the ArgosPlatforms table.
Changing the Collar of an ArgosDeployment will invalidate the Collar value of any files
resulting from processing files containing transmission from PlatformId during the
relevant time period.  Since there is now no collar associated with the PlatformId
(a Collar cannot have two platforms at the same time), the result files for the relevant
date range should be deleted.

## `StartDate, EndDate`

These dates provide the time period during which the ArgosPlatform is deployed on the
Collar.  A null StartDate is an unknown date that is considered to be less than any known
date/time.  A null EndDate is an unknown date/time that is considered to be greater than
any known date.  The StartDate must be before the EndDate.  The StartDate and EndDate must
be before the DisposalDate of the Collar and the ArgosPlatform if any.
Changing the date range of an ArgosDeployment will invalidate CollarFiles built from
transmission that were in the date range, but are now outside the new date range.  There
may also be unprocessed transmissions if the date range has been expanded.

## Business Rules

The following rules are enforced in the insert and update triggers to ensure data
integrity if an elevated account bypasses the insert/update stored procedures.
- Ensure StartDate is before the EndDate.
- Ensure EndDate is before the DisposalDate of the Collar and ArgosPlatform.
- Ensure an ArgosPlatform is not deployed on multiple Collars at the same time.
- Ensure a Collar is not carrying multiple ArgosPlatforms at the same time.

For Gen4 Collars manufactured by Telonics, the manufacturer provides a Telonics Parameter
File (*.tpf) which provides, among other things, the PlatformId and StartDate for each
Collar.  If the Manager loads the TPF file in the database (required to process Collar
data obtained via the Argos system), then the TPF file data can be used to create and
validate ArgosDeployments.  **How should this validation be done?**

The ArgosDeployments table cannot be edited directly, only through stored procedures.
Stored procedures manage the permission validation, and triggers manage the data
validation, and updates to related tables.

## Delete

Only the database user who owns the Collar *AND* owns the platform (actually the related
ArgosProgram) or is an Assistant to either or both owners can delete an ArgosDeployment.
The database will never delete these objects automatically. ArgosDeployment deletes
cannot be done if the ArgosDeployment was used to create a Collar File - see the Special
Note on Referential Integrity in the CollarParameter discussion.  Therefore, if the
client wants to delete a ArgosDeployment, they must first delete related CollarFiles.
Deleting a CollarFile is sufficient to list the parents file in need of reprocessing
(the parent CollarFile will be listed in ArgosPlatformDates but will not have a derived
CollarFile, and no ProcessingIssues to explain it), after a scheduled attempt to
process, the file will have a processing issue to blaming the missing ArgosDeployment.

## Update
  A update can be done by the Manager of the Collar, *or* the Manager of the PlatformId,
  see the discussion above for delete.
  DeploymentId:
    Changes are not allowed.
  PlatformId, Collar, StartDate, EndDate.
    Changes are allowed.  Business rules listed above must be validated. The update should
	be treated as a delete followed by an insert for purposes of managing the processing
	of CollarFiles.  **The UI should warn the user if the changed deployment will leave
	some CollarFile data unprocessed.**

## Insert
Only Investigators can run this SP.  A valid non-null PlatformId and Collar is required.
Business rules listed above must be validated.  Only the Manager of the Collar and
related ArgosProgram can do the insert.  **One of our ArgosPrograms is used by multiple
Managers, so we will relax this second requirement, until we create an 'Assistant'
role.**  StartDate and EndDate default to null.
The new ArgosDeployment may provide new data required to process a CollarFile.
ArgosFilePlatformDates can be scanned to see if the new deployment overlaps with a
CollarFile.  Any overlapping files should be (re)processed.  **Provide an
ability to (re)process only one platform in a file.**


#
# API for changing database State

Create a limited well defined set of methods (stored procedures) that can change the
state of the database in a limited and well defined way.  There are three processes that
will be done outside the database (that should be done in the database)

1. Download Argos data

   1. download all - active programs and active platforms where platform.program.active
                     is null
   2. download user - same as download all, but only for programs and platform.programs
                      where program.investigator = user
   3. download program - download a single program
   4. download platform - download a single platform
   
   Downloading Argos data will change the state in the following way:
   1. Add one or more records to ArgosDownloads for each program/platform attempted
   2. Add one or more Argos web files to CollarFiles table for each successful download.
      Files can be added with local or server side processing depending on the clients
      resources as discussed below.
   
   Ideally, the database would have 4 stored procedures that managed these 5 steps,
   and writing to ArgosFilePlatformDates, ArgosFileProcessingIssues, and ArgosDownloads
   would only happen through these stored procedures.  However there are complication
   in the SQL Server only implementation (chiefly access to external resources), that
   are not fully understood at this time, so I am creating 4 public methods in
   an external library.  These methods can be called be any client applications,
   including a simple wrapper exe that can be called via xp_cmdshell in SQL Server.
   potentially be a SQL Server.  By wrapping all the logic in one set of public
   methods, I ensure that the proper code is always called in the correct manner.
   The downside is that I need to allow insert permissions to tables that should
   not be written to unless part of this controlled procedure.
   
2. Add Argos File to CollarFiles

   1. add with server side processing - client provides only the file, and then SQL
      server is responsible for spawning the code to process the Telonics data in
      the Argos file.  This is the preference for clients that do not have access to
      the necessary Telonics Data Converter software.
   2. Add with client side processing
   
   Regardless of where this occurs, the following must happen
   1. Add the file to CollarFiles table
      - determine the format from the contents
      - need either project, or PI (what list will this file be in)
      - for some file formats, we need a collar id; might be determined from contents 
   2. if the file has Argos transmissions, then add records to ArgosFilePlatformDates
      for each platform in the new file
   3. if the file has Argos transmissions, process each new file as described below

3. Process Argos File

   1. process all existing - processes all the files in the database that should, but do
                             not, have processing results
   2. process new file - file will be a path string, byte array, or byte stream provided
                         by the calling program.  The downloading methods discussed above
                         will provide a byte[] or stream, whereas other clients may
                         provide a file path.
   3. process existing - process a single existing file. file is be an integer id of
                     an existing file in the database.
                     
   Processing an Argos file will change the state in the following way:
   * If the file already exist in the database
      1. Clear any existing ArgosFileProcessingIssues for an existing file
      2. Clear any existing previously derived results in CollarFiles for this file, only
      3. Add one or more derived Telonics files to CollarFiles for each collar/parameter
          combination active for the platform date range in each new file
      4. Add zero or more records to ArgosFileProcessingIssues for each issue encountered
          deriving the Telonics data for each new file
   * If the file is new
      1. Insert a file into the CollarFiles table if not already there

   The processing results are dependent on the state of the database at the time
   a file was processed. In particular, the platform to collar and collar to parameter
   mappings and date ranges of those mappings.  *If the ArgosDeployments, or
   CollarParameters tables are edited, all related files should be checked use
   ArgosFilePlatformDates and reprocess if necessary*


#
# External Applications

*	ArgosDownloader
	-	Reads:
		-	ArgosPrograms (Active, Manager, Manager.Email, UserName, ProgramId, Password
		-	ArgosPlatforms (Active, program.*, PlatformId,
		-	ArgosDownloads (ProgramId, PlatformId, FileId, TimeStamp)
		-	DaysSinceLastDownload
		-	Settings - Reads system settings: `sa_email_*`, `tdc_*`
	-	Executes:
		-	CollarFiles_insert
		-	ArgosDownloads_insert
		
*	ArgosProcessor
	*	Reads:
		-	Projects
		-	CollarFiles
		-	NeverProcessedArgosFiles
		-	GetTelonicsParametersForArgosDates
			-	CollarManufacturer
			-	CollarId
			-	CollarModel
			-	PlatformId
			-	Gen3Period
			-	Contents
			-	Format
			-	StartDate
			-	EndDate
	-	Executes:
		-	ArgosFile_Process
		-	CollarFiles_Insert
		-	ArgosFile_ClearProcessingResults
		-	ArgosFileProcessingIssue_Insert
	

# Non Table Objects

A list of Functions, Procedures and Views used in applications, and their
dependencies. This does not include all the tables/columns used in the output
and the where clause. Other columns may be used to join the tables, these
columns should be obvious.

TVF = Table Valued Function; SVF = Single Value Function; SP = Stored Procedure
	
## AnimalFixesByFile (view)

-	CollarFixes
	-	CollarId
	-	FileId
	-	FixDate
-	CollarDeployments
	-	AnimalId
	-	DeploymentDate
	-	RetrievalDate
-	Projects
	-	ProjectName
-	LookupCollarManufacturers
	-	Name
		
## AnimalLocationSummary (TVF)

-	Locations
	-	ProjectId
	-	AnimalId
	-	FixDate
	-	Location
	-	Status
		
## CollarFixesByFile (TVF)

-	CollarFiles
	-	CollarManufacturer
	-	CollarId
	-	FileId
	-	FileName
-	CollarFixes
	-	CollarManufacturer
	-	CollarId
	-	FixDate
-	LookupFileStatus
	-	Name

## CollarFixSummary (TVF)

-	CollarFixes
	-	CollarManufacturer
	-	CollarId
	-	FixDate

## ConflictingFixes (TVF)

-	CollarFixes
	-	CollarManufacturer
	-	CollarId
	-	FixDate
	-	FixId, HiddenBy, FileId, LineNumber, Lat, Lon

## DaysSinceLastDownload (SVF)

-	ArgosDownloads
	-	ErrorMessage
	-	TimeStamp
		
## GetTelonicsParametersForArgosDates (TVF)

-	ArgosDeployments
	-	CollarManufacturer
	-	CollarId
	-	PlatformId
	-	StartDate
	-	EndDate
-	Collars
	-	CollarModel
-	CollarParameterFiles
	-	Contents
	-	FileName
	-	Format
-	CollarParameters
	-	FileId
	-	Gen3Period
	-	StartDate
	-	EndDate
	
## IsProjectEditor (SVF)

-	Projects
	-	ProjectId
	-	ProjectInvestigator
-	ProjectEditors
	-	Editor

## IsInvestigatorEditor (SVF)

-	ProjectInvestigators
	-	Login
-	ProjectInvestigatorAssistants
	-	ProjectInvestigator
	-	Assistant

# IsFixEditor (SVF)

-	IsProjectEditor
-	CollarFiles
	-	Project
-	CollarFixes
	-	FixId
	
## NextAnimalId (SVF)

-	Animals
	-	AnimalId
-	Projects
	-	ProjectId

## ArgosFile_Process (SP)

-	CollarFiles
	-	FileId
	-	Status
	-	Format
	-	ParentFileId
-	Projects
	-	ProjectId
	-	ProjectInvestigator
-	ProjectEditors
	-	ProjectId
	-	Editor	
-	Settings
	-	Username, Key, Value
- CollarFile_Delete SP
- xp_cmdshell ArgosProcessor.exe

## UnprocessedArgosFile (view)

-	CollarFiles
	-	FileId
	-	FileName
	-	Format
	-	ParentFileId
	-	Project
	-	UploadDate
	-	UserName
