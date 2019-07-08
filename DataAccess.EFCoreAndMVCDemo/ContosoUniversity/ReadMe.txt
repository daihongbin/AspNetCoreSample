//添加以下三个东西 就能使用dotnet ef了
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="2.2.4" />
<DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.3" />
<DotNetCliToolReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Tools" Version="2.0.0" />

//cli命令
//添加迁移
dotnet ef migrations add MaxLengthOnNames

//应用迁移
dotnet ef database update