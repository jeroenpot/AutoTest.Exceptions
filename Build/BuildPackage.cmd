call BuildRelease.cmd

..\Source\AutoTest.Exceptions\.nuget\NuGet.exe pack "..\Source\AutoTest.Exceptions\AutoTest.Exceptions\AutoTest.Exceptions.csproj" -Properties "Configuration=Release" -Version %1
