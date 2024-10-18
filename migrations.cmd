@echo off
REM Step 1: Check if we are in the /Infrastructure directory
if not "%CD%"=="%~dp0Infrastructure" (
    cd Infrastructure
)

REM Step 2: List migrations
echo Listing migrations...
dotnet ef migrations list --startup-project ..\KoiPondConstructionManagement\ > migrations_list.txt

REM Step 3: Get the latest migration and generate the new one
for /f "delims=" %%i in (migrations_list.txt) do set "lastMigration=%%i"
echo Last migration is: %lastMigration%

REM Extract migration number (last V number) and increment it
set migrationPrefix=Init
for /f "tokens=2 delims=_" %%a in ("%lastMigration%") do (
    set migrationSuffix=%%a
)

REM Get the last digit after 'V'
for /f "tokens=2 delims=V" %%b in ("%migrationSuffix%") do (
    set migrationNum=%%b
)

REM Increment the migration number
set /a nextMigrationNum=%migrationNum%+1
set nextMigration=%migrationPrefix%V%nextMigrationNum%

echo New migration will be: %nextMigration%

REM REM Step 4: Add the new migration
REM dotnet ef migrations add %nextMigration% --startup-project ..\KoiPondConstructionManagement\

REM REM Step 5: Update the database
REM echo Updating database...
REM dotnet ef database update --startup-project ..\KoiPondConstructionManagement\

REM echo Done.
REM pause
