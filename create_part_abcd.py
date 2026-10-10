import os

root = r"c:\Users\Usuario\Desktop\proyectos\estudio\BankLab"

files = {
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.sln": """
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Ejemplo.Web.PortalClientes", "Ejemplo.Web.PortalClientes\Ejemplo.Web.PortalClientes.csproj", "{P1_GUID}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Ejemplo.Web.PortalOperaciones", "Ejemplo.Web.PortalOperaciones\Ejemplo.Web.PortalOperaciones.csproj", "{P2_GUID}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Ejemplo.Web.PortalReportes", "Ejemplo.Web.PortalReportes\Ejemplo.Web.PortalReportes.csproj", "{P3_GUID}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{P1_GUID}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{P1_GUID}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{P1_GUID}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{P1_GUID}.Release|Any CPU.Build.0 = Release|Any CPU
		{P2_GUID}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{P2_GUID}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{P2_GUID}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{P2_GUID}.Release|Any CPU.Build.0 = Release|Any CPU
		{P3_GUID}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{P3_GUID}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{P3_GUID}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{P3_GUID}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
EndGlobal
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalClientes\Ejemplo.Web.PortalClientes.csproj": """<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProductVersion>
    </ProductVersion>
    <SchemaVersion>2.0</SchemaVersion>
    <ProjectGuid>{P1_GUID}</ProjectGuid>
    <ProjectTypeGuids>{349c5851-65df-11da-9384-00065b846f21};{fae04ec0-301f-11d3-bf4b-00c04f79efbc}</ProjectTypeGuids>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>Ejemplo.Web.PortalClientes</RootNamespace>
    <AssemblyName>Ejemplo.Web.PortalClientes</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
    <UseIISExpress>true</UseIISExpress>
    <Use64BitIISExpress />
    <IISExpressSSLPort />
    <IISExpressAnonymousAuthentication />
    <IISExpressWindowsAuthentication />
    <IISExpressUseClassicPipelineMode />
    <UseGlobalApplicationHostFile />
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="Microsoft.CSharp" />
    <Reference Include="System.Web.DynamicData" />
    <Reference Include="System.Web.Entity" />
    <Reference Include="System.Web.ApplicationServices" />
    <Reference Include="System.ComponentModel.DataAnnotations" />
    <Reference Include="System" />
    <Reference Include="System.Data" />
    <Reference Include="System.Core" />
    <Reference Include="System.Data.DataSetExtensions" />
    <Reference Include="System.Web.Extensions" />
    <Reference Include="System.Xml.Linq" />
    <Reference Include="System.Drawing" />
    <Reference Include="System.Web" />
    <Reference Include="System.Xml" />
    <Reference Include="System.Configuration" />
    <Reference Include="System.Web.Services" />
    <Reference Include="System.EnterpriseServices" />
    <Reference Include="System.Web.Mvc">
      <HintPath>..\\..\\packages\\Microsoft.AspNet.Mvc.5.2.9\\lib\\net45\\System.Web.Mvc.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.Razor">
      <HintPath>..\\..\\packages\\Microsoft.AspNet.Razor.3.2.9\\lib\\net45\\System.Web.Razor.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.WebPages">
      <HintPath>..\\..\\packages\\Microsoft.AspNet.WebPages.3.2.9\\lib\\net45\\System.Web.WebPages.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.WebPages.Deployment">
      <HintPath>..\\..\\packages\\Microsoft.AspNet.WebPages.3.2.9\\lib\\net45\\System.Web.WebPages.Deployment.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.WebPages.Razor">
      <HintPath>..\\..\\packages\\Microsoft.AspNet.WebPages.3.2.9\\lib\\net45\\System.Web.WebPages.Razor.dll</HintPath>
    </Reference>
  </ItemGroup>
  <ItemGroup>
    <Compile Include="App_Start\\BundleConfig.cs" />
    <Compile Include="App_Start\\FilterConfig.cs" />
    <Compile Include="App_Start\\RouteConfig.cs" />
    <Compile Include="Controllers\\ClientesController.cs" />
    <Compile Include="Controllers\\HomeController.cs" />
    <Compile Include="Global.asax.cs">
      <DependentUpon>Global.asax</DependentUpon>
    </Compile>
    <Compile Include="Properties\\AssemblyInfo.cs" />
  </ItemGroup>
  <ItemGroup>
    <Content Include="Content\\Site.css" />
    <Content Include="Global.asax" />
    <Content Include="Web.config" />
    <Content Include="Views\\Web.config" />
    <Content Include="Views\\_ViewStart.cshtml" />
    <Content Include="Views\\Shared\\_Layout.cshtml" />
    <Content Include="Views\\Home\\Index.cshtml" />
    <Content Include="Views\\Clientes\\Index.cshtml" />
    <Content Include="Views\\Clientes\\Detalle.cshtml" />
    <Content Include="packages.config" />
  </ItemGroup>
  <Import Project="$(MSBuildBinPath)\\Microsoft.CSharp.targets" />
  <Import Project="$(VSToolsPath)\\WebApplications\\Microsoft.WebApplication.targets" Condition="'$(VSToolsPath)' != ''" />
</Project>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalClientes\packages.config": """<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="Microsoft.AspNet.Mvc" version="5.2.9" targetFramework="net48" />
  <package id="Microsoft.AspNet.Razor" version="3.2.9" targetFramework="net48" />
  <package id="Microsoft.AspNet.WebPages" version="3.2.9" targetFramework="net48" />
  <package id="Microsoft.Web.Infrastructure" version="2.0.1" targetFramework="net48" />
</packages>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalClientes\Properties\AssemblyInfo.cs": """using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Ejemplo.Web.PortalClientes")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Banco Ejemplo")]
[assembly: AssemblyProduct("Ejemplo.Web.PortalClientes")]
[assembly: AssemblyCopyright("Copyright © Banco Ejemplo 2024")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]
[assembly: Guid("10000000-0000-0000-0000-000000000001")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalClientes\Global.asax": """<%@ Application Codebehind="Global.asax.cs" Inherits="Ejemplo.Web.PortalClientes.MvcApplication" Language="C#" %>""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalClientes\Global.asax.cs": """using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;

namespace Ejemplo.Web.PortalClientes
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalClientes\App_Start\RouteConfig.cs": """using System.Web.Mvc;
using System.Web.Routing;

namespace Ejemplo.Web.PortalClientes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalClientes\App_Start\FilterConfig.cs": """using System.Web.Mvc;

namespace Ejemplo.Web.PortalClientes
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalClientes\App_Start\BundleConfig.cs": """using System.Web.Optimization;

namespace Ejemplo.Web.PortalClientes
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
        }
    }
}
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalClientes\Controllers\HomeController.cs": """using System.Web.Mvc;

namespace Ejemplo.Web.PortalClientes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalClientes\Controllers\ClientesController.cs": """using System.Web.Mvc;

namespace Ejemplo.Web.PortalClientes.Controllers
{
    public class ClientesController : Controller
    {
        public ActionResult Index(string busqueda, int page = 1)
        {
            ViewBag.Busqueda = busqueda;
            ViewBag.Page = page;
            return View();
        }

        public ActionResult Detalle(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalClientes\Views\Web.config": """<?xml version="1.0"?>
<configuration>
  <configSections>
    <sectionGroup name="system.web.webPages.razor" type="System.Web.WebPages.Razor.Configuration.RazorWebSectionGroup, System.Web.WebPages.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35">
      <section name="host" type="System.Web.WebPages.Razor.Configuration.HostSection, System.Web.WebPages.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" requirePermission="false" />
      <section name="pages" type="System.Web.WebPages.Razor.Configuration.RazorPagesSection, System.Web.WebPages.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" requirePermission="false" />
    </sectionGroup>
  </configSections>
  <system.web.webPages.razor>
    <host factoryType="System.Web.Mvc.MvcWebRazorHostFactory, System.Web.Mvc, Version=5.2.9.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" />
    <pages pageBaseType="System.Web.Mvc.WebViewPage">
      <namespaces>
        <add namespace="System.Web.Mvc" />
        <add namespace="System.Web.Mvc.Ajax" />
        <add namespace="System.Web.Mvc.Html" />
        <add namespace="System.Web.Routing" />
      </namespaces>
    </pages>
  </system.web.webPages.razor>
  <system.web>
    <compilation debug="true" targetFramework="net48"/>
  </system.web>
</configuration>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalClientes\Web.config": """<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <connectionStrings>
    <add name="BankLabDB" connectionString="Server=localhost;Database=BankLabDB;User Id=sa;Password=BankLab2024!;TrustServerCertificate=True;" providerName="System.Data.SqlClient" />
  </connectionStrings>
  <system.web>
    <compilation debug="true" targetFramework="4.8" />
    <httpRuntime targetFramework="4.8" />
  </system.web>
  <runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      <dependentAssembly>
        <assemblyIdentity name="System.Web.Helpers" publicKeyToken="31bf3856ad364e35" />
        <bindingRedirect oldVersion="1.0.0.0-3.0.0.0" newVersion="3.0.0.0" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="System.Web.WebPages" publicKeyToken="31bf3856ad364e35" />
        <bindingRedirect oldVersion="1.0.0.0-3.0.0.0" newVersion="3.0.0.0" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="System.Web.Mvc" publicKeyToken="31bf3856ad364e35" />
        <bindingRedirect oldVersion="1.0.0.0-5.2.9.0" newVersion="5.2.9.0" />
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
</configuration>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalClientes\Content\Site.css": """
body { padding-top: 50px; padding-bottom: 20px; }
.body-content { padding-left: 15px; padding-right: 15px; }
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalClientes\Views\_ViewStart.cshtml": """@{ Layout = "~/Views/Shared/_Layout.cshtml"; }""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalClientes\Views\Shared\_Layout.cshtml": """<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - Banco Ejemplo - Portal Clientes</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <link href="~/Content/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <nav class="navbar navbar-expand-md navbar-dark fixed-top bg-dark">
        <a class="navbar-brand" href="/">Banco Ejemplo - Portal Clientes</a>
    </nav>
    <div class="container body-content">
        @RenderBody()
        <hr />
        <footer>
            <p>&copy; @DateTime.Now.Year - Banco Ejemplo</p>
        </footer>
    </div>
</body>
</html>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalClientes\Views\Home\Index.cshtml": """@{ ViewBag.Title = "Inicio"; }
<div class="jumbotron">
    <h1>Bienvenido al Portal de Clientes</h1>
    <p class="lead">Banco Ejemplo.</p>
</div>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalClientes\Views\Clientes\Index.cshtml": """@{ ViewBag.Title = "Búsqueda de Clientes"; }
<h2>Búsqueda de Clientes</h2>
<form method="get" action="/Clientes/Index">
    <div class="form-group">
        <label>Buscar:</label>
        <input type="text" name="busqueda" class="form-control" value="@ViewBag.Busqueda" />
    </div>
    <button type="submit" class="btn btn-primary">Buscar</button>
</form>
<table class="table mt-3">
    <thead>
        <tr>
            <th>ID</th>
            <th>Nombre</th>
            <th>Acciones</th>
        </tr>
    </thead>
    <tbody>
        <!-- Datos de prueba -->
        <tr>
            <td>1</td>
            <td>Cliente de Prueba</td>
            <td><a href="/Clientes/Detalle/1">Detalle</a></td>
        </tr>
    </tbody>
</table>
<p>Página: @ViewBag.Page</p>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalClientes\Views\Clientes\Detalle.cshtml": """@{ ViewBag.Title = "Detalle del Cliente"; }
<h2>Detalle del Cliente @ViewBag.Id</h2>
<p>Información detallada del cliente y sus productos.</p>
""",

    # ---------------- PortalOperaciones ----------------
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalOperaciones\Ejemplo.Web.PortalOperaciones.csproj": """<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProductVersion>
    </ProductVersion>
    <SchemaVersion>2.0</SchemaVersion>
    <ProjectGuid>{P2_GUID}</ProjectGuid>
    <ProjectTypeGuids>{349c5851-65df-11da-9384-00065b846f21};{fae04ec0-301f-11d3-bf4b-00c04f79efbc}</ProjectTypeGuids>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>Ejemplo.Web.PortalOperaciones</RootNamespace>
    <AssemblyName>Ejemplo.Web.PortalOperaciones</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
    <UseIISExpress>true</UseIISExpress>
    <Use64BitIISExpress />
    <IISExpressSSLPort />
    <IISExpressAnonymousAuthentication />
    <IISExpressWindowsAuthentication />
    <IISExpressUseClassicPipelineMode />
    <UseGlobalApplicationHostFile />
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="Microsoft.CSharp" />
    <Reference Include="System" />
    <Reference Include="System.Web" />
    <Reference Include="System.Web.Mvc">
      <HintPath>..\\..\\packages\\Microsoft.AspNet.Mvc.5.2.9\\lib\\net45\\System.Web.Mvc.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.Razor">
      <HintPath>..\\..\\packages\\Microsoft.AspNet.Razor.3.2.9\\lib\\net45\\System.Web.Razor.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.WebPages">
      <HintPath>..\\..\\packages\\Microsoft.AspNet.WebPages.3.2.9\\lib\\net45\\System.Web.WebPages.dll</HintPath>
    </Reference>
  </ItemGroup>
  <ItemGroup>
    <Compile Include="App_Start\\RouteConfig.cs" />
    <Compile Include="Controllers\\OperacionesController.cs" />
    <Compile Include="Controllers\\CarteraController.cs" />
    <Compile Include="Global.asax.cs">
      <DependentUpon>Global.asax</DependentUpon>
    </Compile>
    <Compile Include="Properties\\AssemblyInfo.cs" />
  </ItemGroup>
  <ItemGroup>
    <Content Include="Global.asax" />
    <Content Include="Web.config" />
    <Content Include="Views\\Web.config" />
    <Content Include="Views\\_ViewStart.cshtml" />
    <Content Include="Views\\Shared\\_Layout.cshtml" />
    <Content Include="Views\\Operaciones\\Index.cshtml" />
    <Content Include="Views\\Operaciones\\Detalle.cshtml" />
    <Content Include="Views\\Operaciones\\Cuotas.cshtml" />
    <Content Include="Views\\Cartera\\Index.cshtml" />
    <Content Include="packages.config" />
  </ItemGroup>
  <Import Project="$(MSBuildBinPath)\\Microsoft.CSharp.targets" />
  <Import Project="$(VSToolsPath)\\WebApplications\\Microsoft.WebApplication.targets" Condition="'$(VSToolsPath)' != ''" />
</Project>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalOperaciones\packages.config": """<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="Microsoft.AspNet.Mvc" version="5.2.9" targetFramework="net48" />
  <package id="Microsoft.AspNet.Razor" version="3.2.9" targetFramework="net48" />
  <package id="Microsoft.AspNet.WebPages" version="3.2.9" targetFramework="net48" />
  <package id="Microsoft.Web.Infrastructure" version="2.0.1" targetFramework="net48" />
</packages>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalOperaciones\Properties\AssemblyInfo.cs": """using System.Reflection;
using System.Runtime.InteropServices;
[assembly: AssemblyTitle("Ejemplo.Web.PortalOperaciones")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Ejemplo.Web.PortalOperaciones")]
[assembly: AssemblyCopyright("Copyright ©  2024")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("20000000-0000-0000-0000-000000000002")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalOperaciones\Global.asax": """<%@ Application Codebehind="Global.asax.cs" Inherits="Ejemplo.Web.PortalOperaciones.MvcApplication" Language="C#" %>""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalOperaciones\Global.asax.cs": """using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ejemplo.Web.PortalOperaciones
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalOperaciones\App_Start\RouteConfig.cs": """using System.Web.Mvc;
using System.Web.Routing;

namespace Ejemplo.Web.PortalOperaciones
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Operaciones", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalOperaciones\Controllers\OperacionesController.cs": """using System.Web.Mvc;

namespace Ejemplo.Web.PortalOperaciones.Controllers
{
    public class OperacionesController : Controller
    {
        public ActionResult Index(string busqueda)
        {
            ViewBag.Busqueda = busqueda;
            return View();
        }

        public ActionResult Detalle(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult Cuotas(int operacionId)
        {
            ViewBag.OperacionId = operacionId;
            return View();
        }
    }
}
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalOperaciones\Controllers\CarteraController.cs": """using System.Web.Mvc;

namespace Ejemplo.Web.PortalOperaciones.Controllers
{
    public class CarteraController : Controller
    {
        public ActionResult Index(string filtro)
        {
            ViewBag.Filtro = filtro;
            return View();
        }
    }
}
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalOperaciones\Views\Web.config": """<?xml version="1.0"?>
<configuration>
  <configSections>
    <sectionGroup name="system.web.webPages.razor" type="System.Web.WebPages.Razor.Configuration.RazorWebSectionGroup, System.Web.WebPages.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35">
      <section name="host" type="System.Web.WebPages.Razor.Configuration.HostSection, System.Web.WebPages.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" requirePermission="false" />
      <section name="pages" type="System.Web.WebPages.Razor.Configuration.RazorPagesSection, System.Web.WebPages.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" requirePermission="false" />
    </sectionGroup>
  </configSections>
  <system.web.webPages.razor>
    <host factoryType="System.Web.Mvc.MvcWebRazorHostFactory, System.Web.Mvc, Version=5.2.9.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" />
    <pages pageBaseType="System.Web.Mvc.WebViewPage">
      <namespaces>
        <add namespace="System.Web.Mvc" />
        <add namespace="System.Web.Mvc.Ajax" />
        <add namespace="System.Web.Mvc.Html" />
        <add namespace="System.Web.Routing" />
      </namespaces>
    </pages>
  </system.web.webPages.razor>
</configuration>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalOperaciones\Web.config": """<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <system.web>
    <compilation debug="true" targetFramework="4.8" />
    <httpRuntime targetFramework="4.8" />
  </system.web>
</configuration>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalOperaciones\Views\_ViewStart.cshtml": """@{ Layout = "~/Views/Shared/_Layout.cshtml"; }""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalOperaciones\Views\Shared\_Layout.cshtml": """<!DOCTYPE html>
<html>
<head>
    <title>@ViewBag.Title - Banco Ejemplo - Portal Operaciones</title>
</head>
<body>
    <div class="container body-content">
        @RenderBody()
    </div>
</body>
</html>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalOperaciones\Views\Operaciones\Index.cshtml": """@{ ViewBag.Title = "Operaciones"; }
<h2>Operaciones</h2>
<p>Buscar: @ViewBag.Busqueda</p>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalOperaciones\Views\Operaciones\Detalle.cshtml": """@{ ViewBag.Title = "Detalle de Operacion"; }
<h2>Detalle @ViewBag.Id</h2>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalOperaciones\Views\Operaciones\Cuotas.cshtml": """@{ ViewBag.Title = "Cuotas"; }
<h2>Cuotas Operacion @ViewBag.OperacionId</h2>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalOperaciones\Views\Cartera\Index.cshtml": """@{ ViewBag.Title = "Cartera"; }
<h2>Reporte de Cartera</h2>
""",

    # ---------------- PortalReportes ----------------
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalReportes\Ejemplo.Web.PortalReportes.csproj": """<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProductVersion>
    </ProductVersion>
    <SchemaVersion>2.0</SchemaVersion>
    <ProjectGuid>{P3_GUID}</ProjectGuid>
    <ProjectTypeGuids>{349c5851-65df-11da-9384-00065b846f21};{fae04ec0-301f-11d3-bf4b-00c04f79efbc}</ProjectTypeGuids>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>Ejemplo.Web.PortalReportes</RootNamespace>
    <AssemblyName>Ejemplo.Web.PortalReportes</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
    <UseIISExpress>true</UseIISExpress>
    <Use64BitIISExpress />
    <IISExpressSSLPort />
    <IISExpressAnonymousAuthentication />
    <IISExpressWindowsAuthentication />
    <IISExpressUseClassicPipelineMode />
    <UseGlobalApplicationHostFile />
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="Microsoft.CSharp" />
    <Reference Include="System" />
    <Reference Include="System.Web" />
    <Reference Include="System.Web.Mvc">
      <HintPath>..\\..\\packages\\Microsoft.AspNet.Mvc.5.2.9\\lib\\net45\\System.Web.Mvc.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.Razor">
      <HintPath>..\\..\\packages\\Microsoft.AspNet.Razor.3.2.9\\lib\\net45\\System.Web.Razor.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.WebPages">
      <HintPath>..\\..\\packages\\Microsoft.AspNet.WebPages.3.2.9\\lib\\net45\\System.Web.WebPages.dll</HintPath>
    </Reference>
  </ItemGroup>
  <ItemGroup>
    <Compile Include="App_Start\\RouteConfig.cs" />
    <Compile Include="Controllers\\ReportesController.cs" />
    <Compile Include="Controllers\\AuditoriaController.cs" />
    <Compile Include="Global.asax.cs">
      <DependentUpon>Global.asax</DependentUpon>
    </Compile>
    <Compile Include="Properties\\AssemblyInfo.cs" />
  </ItemGroup>
  <ItemGroup>
    <Content Include="Global.asax" />
    <Content Include="Web.config" />
    <Content Include="Views\\Web.config" />
    <Content Include="Views\\_ViewStart.cshtml" />
    <Content Include="Views\\Shared\\_Layout.cshtml" />
    <Content Include="Views\\Reportes\\Index.cshtml" />
    <Content Include="Views\\Auditoria\\Index.cshtml" />
    <Content Include="packages.config" />
  </ItemGroup>
  <Import Project="$(MSBuildBinPath)\\Microsoft.CSharp.targets" />
  <Import Project="$(VSToolsPath)\\WebApplications\\Microsoft.WebApplication.targets" Condition="'$(VSToolsPath)' != ''" />
</Project>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalReportes\packages.config": """<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="Microsoft.AspNet.Mvc" version="5.2.9" targetFramework="net48" />
  <package id="Microsoft.AspNet.Razor" version="3.2.9" targetFramework="net48" />
  <package id="Microsoft.AspNet.WebPages" version="3.2.9" targetFramework="net48" />
  <package id="Microsoft.Web.Infrastructure" version="2.0.1" targetFramework="net48" />
</packages>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalReportes\Properties\AssemblyInfo.cs": """using System.Reflection;
using System.Runtime.InteropServices;
[assembly: AssemblyTitle("Ejemplo.Web.PortalReportes")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Ejemplo.Web.PortalReportes")]
[assembly: AssemblyCopyright("Copyright ©  2024")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("30000000-0000-0000-0000-000000000003")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalReportes\Global.asax": """<%@ Application Codebehind="Global.asax.cs" Inherits="Ejemplo.Web.PortalReportes.MvcApplication" Language="C#" %>""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalReportes\Global.asax.cs": """using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ejemplo.Web.PortalReportes
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalReportes\App_Start\RouteConfig.cs": """using System.Web.Mvc;
using System.Web.Routing;

namespace Ejemplo.Web.PortalReportes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Reportes", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalReportes\Controllers\ReportesController.cs": """using System.Web.Mvc;

namespace Ejemplo.Web.PortalReportes.Controllers
{
    public class ReportesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ejecutar(int reporteId)
        {
            ViewBag.ReporteId = reporteId;
            return View();
        }
    }
}
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalReportes\Controllers\AuditoriaController.cs": """using System.Web.Mvc;

namespace Ejemplo.Web.PortalReportes.Controllers
{
    public class AuditoriaController : Controller
    {
        public ActionResult Index(string busqueda)
        {
            ViewBag.Busqueda = busqueda;
            return View();
        }
    }
}
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalReportes\Views\Web.config": """<?xml version="1.0"?>
<configuration>
  <configSections>
    <sectionGroup name="system.web.webPages.razor" type="System.Web.WebPages.Razor.Configuration.RazorWebSectionGroup, System.Web.WebPages.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35">
      <section name="host" type="System.Web.WebPages.Razor.Configuration.HostSection, System.Web.WebPages.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" requirePermission="false" />
      <section name="pages" type="System.Web.WebPages.Razor.Configuration.RazorPagesSection, System.Web.WebPages.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" requirePermission="false" />
    </sectionGroup>
  </configSections>
  <system.web.webPages.razor>
    <host factoryType="System.Web.Mvc.MvcWebRazorHostFactory, System.Web.Mvc, Version=5.2.9.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" />
    <pages pageBaseType="System.Web.Mvc.WebViewPage">
      <namespaces>
        <add namespace="System.Web.Mvc" />
        <add namespace="System.Web.Mvc.Ajax" />
        <add namespace="System.Web.Mvc.Html" />
        <add namespace="System.Web.Routing" />
      </namespaces>
    </pages>
  </system.web.webPages.razor>
</configuration>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalReportes\Web.config": """<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <system.web>
    <compilation debug="true" targetFramework="4.8" />
    <httpRuntime targetFramework="4.8" />
  </system.web>
</configuration>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalReportes\Views\_ViewStart.cshtml": """@{ Layout = "~/Views/Shared/_Layout.cshtml"; }""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalReportes\Views\Shared\_Layout.cshtml": """<!DOCTYPE html>
<html>
<head>
    <title>@ViewBag.Title - Banco Ejemplo - Portal Reportes</title>
</head>
<body>
    <div class="container body-content">
        @RenderBody()
    </div>
</body>
</html>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalReportes\Views\Reportes\Index.cshtml": """@{ ViewBag.Title = "Reportes"; }
<h2>Reportes</h2>
""",
    r"src\legacy\Ejemplo.Web\Ejemplo.Web.PortalReportes\Views\Auditoria\Index.cshtml": """@{ ViewBag.Title = "Auditoria"; }
<h2>Auditoria</h2>
""",

    # ---------------- PART B: Ejemplo.Servicios ----------------
    r"src\legacy\Ejemplo.Servicios\Ejemplo.Servicios.sln": """
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Ejemplo.Servicios.ProcesadorPagos", "Ejemplo.Servicios.ProcesadorPagos\Ejemplo.Servicios.ProcesadorPagos.csproj", "{S1_GUID}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Ejemplo.Servicios.Comun", "Ejemplo.Servicios.Comun\Ejemplo.Servicios.Comun.csproj", "{S2_GUID}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{S1_GUID}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{S1_GUID}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{S1_GUID}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{S1_GUID}.Release|Any CPU.Build.0 = Release|Any CPU
		{S2_GUID}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{S2_GUID}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{S2_GUID}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{S2_GUID}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
EndGlobal
""",
    r"src\legacy\Ejemplo.Servicios\Ejemplo.Servicios.ProcesadorPagos\Ejemplo.Servicios.ProcesadorPagos.csproj": """<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{S1_GUID}</ProjectGuid>
    <OutputType>Exe</OutputType>
    <RootNamespace>Ejemplo.Servicios.ProcesadorPagos</RootNamespace>
    <AssemblyName>Ejemplo.Servicios.ProcesadorPagos</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
    <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="System" />
    <Reference Include="System.Configuration" />
    <Reference Include="System.Configuration.Install" />
    <Reference Include="System.Core" />
    <Reference Include="System.Management" />
    <Reference Include="System.ServiceProcess" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="ProcesadorPagosService.cs">
      <SubType>Component</SubType>
    </Compile>
    <Compile Include="ProcesadorEngine.cs" />
    <Compile Include="ProcesadorPagosInstaller.cs">
      <SubType>Component</SubType>
    </Compile>
    <Compile Include="Program.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <ItemGroup>
    <None Include="App.config" />
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>
""",
    r"src\legacy\Ejemplo.Servicios\Ejemplo.Servicios.ProcesadorPagos\Properties\AssemblyInfo.cs": """using System.Reflection;
using System.Runtime.InteropServices;
[assembly: AssemblyTitle("Ejemplo.Servicios.ProcesadorPagos")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Ejemplo.Servicios.ProcesadorPagos")]
[assembly: AssemblyCopyright("Copyright ©  2024")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("40000000-0000-0000-0000-000000000004")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
""",
    r"src\legacy\Ejemplo.Servicios\Ejemplo.Servicios.ProcesadorPagos\ProcesadorPagosService.cs": """using System.ServiceProcess;
using System.Timers;

namespace Ejemplo.Servicios.ProcesadorPagos
{
    public partial class ProcesadorPagosService : ServiceBase
    {
        private Timer _timer;
        private ProcesadorEngine _engine;

        public ProcesadorPagosService()
        {
            ServiceName = "Ejemplo.Servicios.ProcesadorPagos";
        }

        protected override void OnStart(string[] args)
        {
            _engine = new ProcesadorEngine();
            _timer = new Timer(30000); // 30 segundos
            _timer.Elapsed += (sender, e) => _engine.ScanDirectory();
            _timer.Start();
        }

        protected override void OnStop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }
        }
    }
}
""",
    r"src\legacy\Ejemplo.Servicios\Ejemplo.Servicios.ProcesadorPagos\ProcesadorEngine.cs": """using System;
using System.IO;

namespace Ejemplo.Servicios.ProcesadorPagos
{
    public class ProcesadorEngine
    {
        public void ScanDirectory()
        {
            string dirEntrada = System.Configuration.ConfigurationManager.AppSettings["DirectorioEntrada"];
            string dirProcesados = System.Configuration.ConfigurationManager.AppSettings["DirectorioProcesados"];

            if (!Directory.Exists(dirEntrada)) return;
            if (!Directory.Exists(dirProcesados)) Directory.CreateDirectory(dirProcesados);

            string[] files = Directory.GetFiles(dirEntrada, "*.csv");
            foreach (var file in files)
            {
                ProcessFile(file, dirProcesados);
            }
        }

        public void ProcessFile(string filePath, string dirProcesados)
        {
            Console.WriteLine($"Procesando archivo: {filePath}");
            // Lógica de lectura de CSV, validación y batch
            // Mover a procesados
            string fileName = Path.GetFileName(filePath);
            string dest = Path.Combine(dirProcesados, fileName);
            if (!File.Exists(dest))
            {
                File.Move(filePath, dest);
                Console.WriteLine($"Archivo procesado y movido a: {dest}");
            }
        }
    }
}
""",
    r"src\legacy\Ejemplo.Servicios\Ejemplo.Servicios.ProcesadorPagos\ProcesadorPagosInstaller.cs": """using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Ejemplo.Servicios.ProcesadorPagos
{
    [RunInstaller(true)]
    public class ProcesadorPagosInstaller : Installer
    {
        public ProcesadorPagosInstaller()
        {
            var processInstaller = new ServiceProcessInstaller();
            var serviceInstaller = new ServiceInstaller();

            processInstaller.Account = ServiceAccount.LocalService;

            serviceInstaller.ServiceName = "ProcesadorPagosBancoEjemplo";
            serviceInstaller.DisplayName = "Procesador de Pagos Banco Ejemplo";
            serviceInstaller.Description = "Servicio de procesamiento de archivos de pagos CSV";
            serviceInstaller.StartType = ServiceStartMode.Manual;

            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
""",
    r"src\legacy\Ejemplo.Servicios\Ejemplo.Servicios.ProcesadorPagos\Program.cs": """using System;
using System.ServiceProcess;
using System.Linq;

namespace Ejemplo.Servicios.ProcesadorPagos
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Contains("--console"))
            {
                Console.WriteLine("Iniciando en modo consola...");
                var engine = new ProcesadorEngine();
                engine.ScanDirectory();
                Console.WriteLine("Procesamiento completado. Presione cualquier tecla para salir.");
                Console.ReadKey();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new ProcesadorPagosService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
""",
    r"src\legacy\Ejemplo.Servicios\Ejemplo.Servicios.ProcesadorPagos\App.config": """<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <connectionStrings>
    <add name="BankLabDB" connectionString="Server=localhost;Database=BankLabDB;User Id=sa;Password=BankLab2024!;TrustServerCertificate=True;" providerName="System.Data.SqlClient" />
  </connectionStrings>
  <appSettings>
    <add key="DirectorioEntrada" value="C:\BankLab\Pagos\Entrada" />
    <add key="DirectorioProcesados" value="C:\BankLab\Pagos\Procesados" />
    <add key="IntervaloSegundos" value="30" />
    <add key="TimeoutSegundos" value="30" />
  </appSettings>
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8" />
  </startup>
</configuration>
""",

    # ---------------- Ejemplo.Servicios.Comun ----------------
    r"src\legacy\Ejemplo.Servicios\Ejemplo.Servicios.Comun\Ejemplo.Servicios.Comun.csproj": """<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{S2_GUID}</ProjectGuid>
    <OutputType>Library</OutputType>
    <RootNamespace>Ejemplo.Servicios.Comun</RootNamespace>
    <AssemblyName>Ejemplo.Servicios.Comun</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="System" />
    <Reference Include="System.Core" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="ServiceLogger.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>
""",
    r"src\legacy\Ejemplo.Servicios\Ejemplo.Servicios.Comun\Properties\AssemblyInfo.cs": """using System.Reflection;
using System.Runtime.InteropServices;
[assembly: AssemblyTitle("Ejemplo.Servicios.Comun")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Ejemplo.Servicios.Comun")]
[assembly: AssemblyCopyright("Copyright ©  2024")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("50000000-0000-0000-0000-000000000005")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
""",
    r"src\legacy\Ejemplo.Servicios\Ejemplo.Servicios.Comun\ServiceLogger.cs": """using System;

namespace Ejemplo.Servicios.Comun
{
    public class ServiceLogger
    {
        public static void LogInfo(string serviceName, string message, int eventId)
        {
            Console.WriteLine($"[INFO] {serviceName} ({eventId}): {message}");
        }

        public static void LogError(string serviceName, string message, int eventId, Exception ex)
        {
            Console.WriteLine($"[ERROR] {serviceName} ({eventId}): {message} - {ex.Message}");
        }
    }
}
""",

    # ---------------- PART C: Prometeus.Utilities ----------------
    r"src\legacy\Prometeus.Utilities\Prometeus.Utilities.sln": """
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Prometeus.Utilities.ImportadorCSV", "Prometeus.Utilities.ImportadorCSV\Prometeus.Utilities.ImportadorCSV.csproj", "{U1_GUID}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Prometeus.Utilities.ValidadorDatos", "Prometeus.Utilities.ValidadorDatos\Prometeus.Utilities.ValidadorDatos.csproj", "{U2_GUID}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Prometeus.Utilities.Exportador", "Prometeus.Utilities.Exportador\Prometeus.Utilities.Exportador.csproj", "{U3_GUID}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Prometeus.Utilities.Diagnostico", "Prometeus.Utilities.Diagnostico\Prometeus.Utilities.Diagnostico.csproj", "{U4_GUID}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{U1_GUID}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{U1_GUID}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{U1_GUID}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{U1_GUID}.Release|Any CPU.Build.0 = Release|Any CPU
		{U2_GUID}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{U2_GUID}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{U2_GUID}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{U2_GUID}.Release|Any CPU.Build.0 = Release|Any CPU
		{U3_GUID}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{U3_GUID}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{U3_GUID}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{U3_GUID}.Release|Any CPU.Build.0 = Release|Any CPU
		{U4_GUID}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{U4_GUID}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{U4_GUID}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{U4_GUID}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
EndGlobal
""",
    r"src\legacy\Prometeus.Utilities\Prometeus.Utilities.ImportadorCSV\Prometeus.Utilities.ImportadorCSV.csproj": """<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{U1_GUID}</ProjectGuid>
    <OutputType>Exe</OutputType>
    <RootNamespace>Prometeus.Utilities.ImportadorCSV</RootNamespace>
    <AssemblyName>Prometeus.Utilities.ImportadorCSV</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="System" />
    <Reference Include="System.Core" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="ImportadorCSV.cs" />
    <Compile Include="Program.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>
""",
    r"src\legacy\Prometeus.Utilities\Prometeus.Utilities.ImportadorCSV\Properties\AssemblyInfo.cs": """using System.Reflection;
using System.Runtime.InteropServices;
[assembly: AssemblyTitle("Prometeus.Utilities.ImportadorCSV")]
[assembly: AssemblyVersion("1.0.0.0")]
""",
    r"src\legacy\Prometeus.Utilities\Prometeus.Utilities.ImportadorCSV\ImportadorCSV.cs": """using System;
using System.IO;

namespace Prometeus.Utilities.ImportadorCSV
{
    public class Importador
    {
        public void ImportarArchivo(string path)
        {
            if (File.Exists(path))
            {
                var lineas = File.ReadAllLines(path);
                foreach (var linea in lineas)
                {
                    Console.WriteLine($"Validando e importando línea: {linea}");
                }
            }
        }
    }
}
""",
    r"src\legacy\Prometeus.Utilities\Prometeus.Utilities.ImportadorCSV\Program.cs": """using System;

namespace Prometeus.Utilities.ImportadorCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var importador = new Importador();
                importador.ImportarArchivo(args[0]);
                Console.WriteLine("Resumen: Procesamiento completado.");
            }
            else
            {
                Console.WriteLine("Proporcione la ruta del archivo CSV.");
            }
        }
    }
}
""",
    r"src\legacy\Prometeus.Utilities\Prometeus.Utilities.ValidadorDatos\Prometeus.Utilities.ValidadorDatos.csproj": """<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{U2_GUID}</ProjectGuid>
    <OutputType>Library</OutputType>
    <RootNamespace>Prometeus.Utilities.ValidadorDatos</RootNamespace>
    <AssemblyName>Prometeus.Utilities.ValidadorDatos</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="System" />
    <Reference Include="System.Core" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="ValidadorEntrada.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>
""",
    r"src\legacy\Prometeus.Utilities\Prometeus.Utilities.ValidadorDatos\Properties\AssemblyInfo.cs": """using System.Reflection;
using System.Runtime.InteropServices;
[assembly: AssemblyTitle("Prometeus.Utilities.ValidadorDatos")]
[assembly: AssemblyVersion("1.0.0.0")]
""",
    r"src\legacy\Prometeus.Utilities\Prometeus.Utilities.ValidadorDatos\ValidadorEntrada.cs": """using System;
using System.IO;

namespace Prometeus.Utilities.ValidadorDatos
{
    public class ValidadorEntrada
    {
        public bool ValidarArchivoCSV(string path)
        {
            return File.Exists(path) && Path.GetExtension(path).Equals(".csv", StringComparison.OrdinalIgnoreCase);
        }

        public bool ValidarFormatoLinea(string linea, string[] expectedColumns)
        {
            var columnas = linea.Split(',');
            return columnas.Length == expectedColumns.Length;
        }

        public bool ValidarEncoding(string path)
        {
            return true;
        }
    }
}
""",
    r"src\legacy\Prometeus.Utilities\Prometeus.Utilities.Exportador\Prometeus.Utilities.Exportador.csproj": """<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{U3_GUID}</ProjectGuid>
    <OutputType>Library</OutputType>
    <RootNamespace>Prometeus.Utilities.Exportador</RootNamespace>
    <AssemblyName>Prometeus.Utilities.Exportador</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="System" />
    <Reference Include="System.Core" />
    <Reference Include="System.Data" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="ExportadorResultados.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>
""",
    r"src\legacy\Prometeus.Utilities\Prometeus.Utilities.Exportador\Properties\AssemblyInfo.cs": """using System.Reflection;
using System.Runtime.InteropServices;
[assembly: AssemblyTitle("Prometeus.Utilities.Exportador")]
[assembly: AssemblyVersion("1.0.0.0")]
""",
    r"src\legacy\Prometeus.Utilities\Prometeus.Utilities.Exportador\ExportadorResultados.cs": """using System;
using System.Data;
using System.IO;

namespace Prometeus.Utilities.Exportador
{
    public class ExportadorResultados
    {
        public void ExportarCSV(DataTable table, string filePath)
        {
            File.WriteAllText(filePath, "Exportado a CSV");
        }

        public void ExportarHTML(DataTable table, string filePath, string titulo)
        {
            File.WriteAllText(filePath, $"<html><body><h1>{titulo}</h1></body></html>");
        }

        public void ExportarTextoPlano(DataTable table, string filePath)
        {
            File.WriteAllText(filePath, "Exportado a Texto Plano");
        }
    }
}
""",
    r"src\legacy\Prometeus.Utilities\Prometeus.Utilities.Diagnostico\Prometeus.Utilities.Diagnostico.csproj": """<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{U4_GUID}</ProjectGuid>
    <OutputType>Exe</OutputType>
    <RootNamespace>Prometeus.Utilities.Diagnostico</RootNamespace>
    <AssemblyName>Prometeus.Utilities.Diagnostico</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="System" />
    <Reference Include="System.Core" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="DiagnosticoConfig.cs" />
    <Compile Include="Program.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>
""",
    r"src\legacy\Prometeus.Utilities\Prometeus.Utilities.Diagnostico\Properties\AssemblyInfo.cs": """using System.Reflection;
using System.Runtime.InteropServices;
[assembly: AssemblyTitle("Prometeus.Utilities.Diagnostico")]
[assembly: AssemblyVersion("1.0.0.0")]
""",
    r"src\legacy\Prometeus.Utilities\Prometeus.Utilities.Diagnostico\DiagnosticoConfig.cs": """using System;

namespace Prometeus.Utilities.Diagnostico
{
    public class DiagnosticoConfig
    {
        public bool CheckConnectionString() { return true; }
        public bool CheckDBAccessible() { return true; }
        public bool CheckWCFHost() { return true; }
        public bool CheckDirectories() { return true; }
        public bool CheckLogFileWritable() { return true; }
    }
}
""",
    r"src\legacy\Prometeus.Utilities\Prometeus.Utilities.Diagnostico\Program.cs": """using System;

namespace Prometeus.Utilities.Diagnostico
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new DiagnosticoConfig();
            Console.ForegroundColor = config.CheckConnectionString() ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine("Connection String OK");

            Console.ForegroundColor = config.CheckDBAccessible() ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine("DB Accessible");
            
            Console.ResetColor();
        }
    }
}
""",

    # ---------------- PART D: Ejemplo.Tests ----------------
    r"tests\Ejemplo.Tests.sln": """
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Ejemplo.Tests.BLL", "Ejemplo.Tests.BLL\Ejemplo.Tests.BLL.csproj", "{T1_GUID}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Ejemplo.Tests.DAT", "Ejemplo.Tests.DAT\Ejemplo.Tests.DAT.csproj", "{T2_GUID}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Ejemplo.Tests.Validaciones", "Ejemplo.Tests.Validaciones\Ejemplo.Tests.Validaciones.csproj", "{T3_GUID}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{T1_GUID}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{T1_GUID}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{T1_GUID}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{T1_GUID}.Release|Any CPU.Build.0 = Release|Any CPU
		{T2_GUID}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{T2_GUID}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{T2_GUID}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{T2_GUID}.Release|Any CPU.Build.0 = Release|Any CPU
		{T3_GUID}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{T3_GUID}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{T3_GUID}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{T3_GUID}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
EndGlobal
""",
    r"tests\Ejemplo.Tests.BLL\Ejemplo.Tests.BLL.csproj": """<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="..\..\packages\MSTest.TestAdapter.3.1.1\build\net462\MSTest.TestAdapter.props" Condition="Exists('..\..\packages\MSTest.TestAdapter.3.1.1\build\net462\MSTest.TestAdapter.props')" />
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{T1_GUID}</ProjectGuid>
    <OutputType>Library</OutputType>
    <RootNamespace>Ejemplo.Tests.BLL</RootNamespace>
    <AssemblyName>Ejemplo.Tests.BLL</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="Microsoft.VisualStudio.TestPlatform.TestFramework, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL">
      <HintPath>..\..\packages\MSTest.TestFramework.3.1.1\lib\net462\Microsoft.VisualStudio.TestPlatform.TestFramework.dll</HintPath>
    </Reference>
    <Reference Include="Microsoft.VisualStudio.TestPlatform.TestFramework.Extensions, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL">
      <HintPath>..\..\packages\MSTest.TestFramework.3.1.1\lib\net462\Microsoft.VisualStudio.TestPlatform.TestFramework.Extensions.dll</HintPath>
    </Reference>
    <Reference Include="System" />
    <Reference Include="System.Core" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="ClientesBLLTests.cs" />
    <Compile Include="PagosBLLTests.cs" />
    <Compile Include="CreditosBLLTests.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <ItemGroup>
    <None Include="packages.config" />
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>
""",
    r"tests\Ejemplo.Tests.BLL\packages.config": """<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="MSTest.TestAdapter" version="3.1.1" targetFramework="net48" />
  <package id="MSTest.TestFramework" version="3.1.1" targetFramework="net48" />
</packages>
""",
    r"tests\Ejemplo.Tests.BLL\Properties\AssemblyInfo.cs": """using System.Reflection;
using System.Runtime.InteropServices;
[assembly: AssemblyTitle("Ejemplo.Tests.BLL")]
[assembly: AssemblyVersion("1.0.0.0")]
""",
    r"tests\Ejemplo.Tests.BLL\ClientesBLLTests.cs": """using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ejemplo.Tests.BLL
{
    [TestClass]
    public class ClientesBLLTests
    {
        [TestMethod]
        public void ConsultarCliente_ConIdentificacionInvalida_RetornaError() { }

        [TestMethod]
        public void ConsultarCliente_ConIdNegativo_RetornaError() { }

        [TestMethod]
        public void RegistrarCliente_SinNombre_RetornaError() { }

        [TestMethod]
        public void RegistrarCliente_ConDatosValidos_NoLanzaExcepcionDeValidacion() { }
    }
}
""",
    r"tests\Ejemplo.Tests.BLL\PagosBLLTests.cs": """using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ejemplo.Tests.BLL
{
    [TestClass]
    public class PagosBLLTests
    {
        [TestMethod]
        public void RegistrarPago_MontoNegativo_RetornaError() { }

        [TestMethod]
        public void RegistrarPago_MontoZero_RetornaError() { }

        [TestMethod]
        public void RegistrarPago_OperacionIdInvalido_RetornaError() { }

        [TestMethod]
        public void RegistrarPago_TipoPagoInvalido_RetornaError() { }
    }
}
""",
    r"tests\Ejemplo.Tests.BLL\CreditosBLLTests.cs": """using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ejemplo.Tests.BLL
{
    [TestClass]
    public class CreditosBLLTests
    {
        [TestMethod]
        public void ConsultarOperacion_SinParametros_RetornaError() { }

        [TestMethod]
        public void ConsultarCuotas_OperacionIdInvalido_RetornaError() { }
    }
}
""",
    r"tests\Ejemplo.Tests.DAT\Ejemplo.Tests.DAT.csproj": """<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="..\..\packages\MSTest.TestAdapter.3.1.1\build\net462\MSTest.TestAdapter.props" Condition="Exists('..\..\packages\MSTest.TestAdapter.3.1.1\build\net462\MSTest.TestAdapter.props')" />
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{T2_GUID}</ProjectGuid>
    <OutputType>Library</OutputType>
    <RootNamespace>Ejemplo.Tests.DAT</RootNamespace>
    <AssemblyName>Ejemplo.Tests.DAT</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="Microsoft.VisualStudio.TestPlatform.TestFramework, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL">
      <HintPath>..\..\packages\MSTest.TestFramework.3.1.1\lib\net462\Microsoft.VisualStudio.TestPlatform.TestFramework.dll</HintPath>
    </Reference>
    <Reference Include="Microsoft.VisualStudio.TestPlatform.TestFramework.Extensions, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL">
      <HintPath>..\..\packages\MSTest.TestFramework.3.1.1\lib\net462\Microsoft.VisualStudio.TestPlatform.TestFramework.Extensions.dll</HintPath>
    </Reference>
    <Reference Include="System" />
    <Reference Include="System.Core" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="ConexionHelperTests.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <ItemGroup>
    <None Include="packages.config" />
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>
""",
    r"tests\Ejemplo.Tests.DAT\packages.config": """<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="MSTest.TestAdapter" version="3.1.1" targetFramework="net48" />
  <package id="MSTest.TestFramework" version="3.1.1" targetFramework="net48" />
</packages>
""",
    r"tests\Ejemplo.Tests.DAT\Properties\AssemblyInfo.cs": """using System.Reflection;
using System.Runtime.InteropServices;
[assembly: AssemblyTitle("Ejemplo.Tests.DAT")]
[assembly: AssemblyVersion("1.0.0.0")]
""",
    r"tests\Ejemplo.Tests.DAT\ConexionHelperTests.cs": """using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ejemplo.Tests.DAT
{
    [TestClass]
    public class ConexionHelperTests
    {
        [TestMethod]
        public void ObtenerConexion_SinConfiguracion_LanzaExcepcion() { }

        [TestMethod]
        public void CrearComandoSP_RetornaComandoCorrecto() { }
    }
}
""",
    r"tests\Ejemplo.Tests.Validaciones\Ejemplo.Tests.Validaciones.csproj": """<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="..\..\packages\MSTest.TestAdapter.3.1.1\build\net462\MSTest.TestAdapter.props" Condition="Exists('..\..\packages\MSTest.TestAdapter.3.1.1\build\net462\MSTest.TestAdapter.props')" />
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{T3_GUID}</ProjectGuid>
    <OutputType>Library</OutputType>
    <RootNamespace>Ejemplo.Tests.Validaciones</RootNamespace>
    <AssemblyName>Ejemplo.Tests.Validaciones</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="Microsoft.VisualStudio.TestPlatform.TestFramework, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL">
      <HintPath>..\..\packages\MSTest.TestFramework.3.1.1\lib\net462\Microsoft.VisualStudio.TestPlatform.TestFramework.dll</HintPath>
    </Reference>
    <Reference Include="Microsoft.VisualStudio.TestPlatform.TestFramework.Extensions, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL">
      <HintPath>..\..\packages\MSTest.TestFramework.3.1.1\lib\net462\Microsoft.VisualStudio.TestPlatform.TestFramework.Extensions.dll</HintPath>
    </Reference>
    <Reference Include="System" />
    <Reference Include="System.Core" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="ValidadorIdentificacionTests.cs" />
    <Compile Include="ValidadorMontoTests.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <ItemGroup>
    <None Include="packages.config" />
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>
""",
    r"tests\Ejemplo.Tests.Validaciones\packages.config": """<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="MSTest.TestAdapter" version="3.1.1" targetFramework="net48" />
  <package id="MSTest.TestFramework" version="3.1.1" targetFramework="net48" />
</packages>
""",
    r"tests\Ejemplo.Tests.Validaciones\Properties\AssemblyInfo.cs": """using System.Reflection;
using System.Runtime.InteropServices;
[assembly: AssemblyTitle("Ejemplo.Tests.Validaciones")]
[assembly: AssemblyVersion("1.0.0.0")]
""",
    r"tests\Ejemplo.Tests.Validaciones\ValidadorIdentificacionTests.cs": """using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ejemplo.Tests.Validaciones
{
    [TestClass]
    public class ValidadorIdentificacionTests
    {
        [TestMethod]
        public void ValidarCC_Valido() { }

        [TestMethod]
        public void ValidarCC_Invalido() { }
    }
}
""",
    r"tests\Ejemplo.Tests.Validaciones\ValidadorMontoTests.cs": """using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ejemplo.Tests.Validaciones
{
    [TestClass]
    public class ValidadorMontoTests
    {
        [TestMethod]
        public void ValidarPositivo() { }

        [TestMethod]
        public void ValidarNegativo() { }

        [TestMethod]
        public void ValidarCero() { }
    }
}
"""
}

for path, content in files.items():
    full_path = os.path.join(root, path)
    os.makedirs(os.path.dirname(full_path), exist_ok=True)
    with open(full_path, "w", encoding="utf-8") as f:
        f.write(content.strip() + "\\n")

print("Files created successfully.")
