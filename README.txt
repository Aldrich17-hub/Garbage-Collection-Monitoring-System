SETUP
1. XAMPP Control Panel -> start MySQL (and Apache for phpMyAdmin).
2. phpMyAdmin -> Import -> choose database.sql -> Go.
3. Visual Studio 2026 -> create/open a Windows Forms App (.NET 8) named GarbageCollectionMonitoringSystem,
   or open GarbageCollectionMonitoringSystem.csproj directly. Copy all .cs files into the project.
4. NuGet package: MySql.Data (already referenced in the .csproj; restore if prompted).
5. Run. Test logins:
   juan.resident  / Resident123!   -> Resident dashboard
   maria.official / Official123!   -> Official dashboard
   marco.driver   / Driver123!     -> Driver dashboard
