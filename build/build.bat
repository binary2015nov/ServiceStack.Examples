SET MSBUILD="C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\MSBuild.exe"

%MSBUILD% /p:Configuration=Release ..\src\AllExamples.sln
