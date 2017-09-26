REM SET BUILD=Debug
SET BUILD=Release

COPY "..\..\ServiceStack\src\ServiceStack.Interfaces\bin\%BUILD%\portable40-net40+sl5+win8+wp8+wpa81\ServiceStack.Interfaces.*"

COPY ..\..\ServiceStack.Text\src\ServiceStack.Text\bin\%BUILD%\net45\ServiceStack.Text.*

COPY ..\..\ServiceStack\src\ServiceStack.Client\bin\%BUILD%\net45\ServiceStack.Client.*

COPY ..\..\ServiceStack\src\ServiceStack.Common\bin\%BUILD%\net45\ServiceStack.Common.*

COPY ..\..\ServiceStack\src\ServiceStack\bin\%BUILD%\net45\ServiceStack.dll
COPY ..\..\ServiceStack\src\ServiceStack\bin\%BUILD%\net45\ServiceStack.xml

COPY ..\..\ServiceStack.Redis\src\ServiceStack.Redis\bin\%BUILD%\net45\ServiceStack.Redis.*

