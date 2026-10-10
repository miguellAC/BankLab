import os

base_path = r"c:\Users\Usuario\Desktop\proyectos\estudio\BankLab\src\legacy\Ejemplo.WCFServicios"

files = {
"Ejemplo.WCFServicios.sln": r"""
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31903.59
MinimumVisualStudioVersion = 10.0.40219.1
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Ejemplo.WCF.Contratos", "Ejemplo.WCF.Contratos\Ejemplo.WCF.Contratos.csproj", "{11111111-2222-3333-4444-555555555551}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Ejemplo.WCF.Servicios", "Ejemplo.WCF.Servicios\Ejemplo.WCF.Servicios.csproj", "{11111111-2222-3333-4444-555555555552}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Ejemplo.WCF.Host", "Ejemplo.WCF.Host\Ejemplo.WCF.Host.csproj", "{11111111-2222-3333-4444-555555555553}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Ejemplo.WCF.Agentes", "Ejemplo.WCF.Agentes\Ejemplo.WCF.Agentes.csproj", "{11111111-2222-3333-4444-555555555554}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{11111111-2222-3333-4444-555555555551}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{11111111-2222-3333-4444-555555555551}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{11111111-2222-3333-4444-555555555551}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{11111111-2222-3333-4444-555555555551}.Release|Any CPU.Build.0 = Release|Any CPU
		{11111111-2222-3333-4444-555555555552}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{11111111-2222-3333-4444-555555555552}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{11111111-2222-3333-4444-555555555552}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{11111111-2222-3333-4444-555555555552}.Release|Any CPU.Build.0 = Release|Any CPU
		{11111111-2222-3333-4444-555555555553}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{11111111-2222-3333-4444-555555555553}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{11111111-2222-3333-4444-555555555553}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{11111111-2222-3333-4444-555555555553}.Release|Any CPU.Build.0 = Release|Any CPU
		{11111111-2222-3333-4444-555555555554}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{11111111-2222-3333-4444-555555555554}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{11111111-2222-3333-4444-555555555554}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{11111111-2222-3333-4444-555555555554}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
EndGlobal
""",
"Ejemplo.WCF.Contratos/Ejemplo.WCF.Contratos.csproj": r"""<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{11111111-2222-3333-4444-555555555551}</ProjectGuid>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>Ejemplo.WCF.Contratos</RootNamespace>
    <AssemblyName>Ejemplo.WCF.Contratos</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
    <Deterministic>true</Deterministic>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="System" />
    <Reference Include="System.Core" />
    <Reference Include="System.Runtime.Serialization" />
    <Reference Include="System.ServiceModel" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="IClientesServicio.cs" />
    <Compile Include="ICuentasServicio.cs" />
    <Compile Include="ICreditosServicio.cs" />
    <Compile Include="IPagosServicio.cs" />
    <Compile Include="ICarteraServicio.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\..\Ejemplo.Comun\Ejemplo.Comun.DTOs\Ejemplo.Comun.DTOs.csproj">
      <Project>{00000000-0000-0000-0000-000000000001}</Project>
      <Name>Ejemplo.Comun.DTOs</Name>
    </ProjectReference>
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>""",
"Ejemplo.WCF.Contratos/Properties/AssemblyInfo.cs": r"""using System.Reflection;
using System.Runtime.InteropServices;
[assembly: AssemblyTitle("Ejemplo.WCF.Contratos")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Ejemplo.WCF.Contratos")]
[assembly: AssemblyCopyright("Copyright © 2024")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("11111111-2222-3333-4444-555555555551")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
""",
"Ejemplo.WCF.Contratos/IClientesServicio.cs": r"""using System.ServiceModel;
using Ejemplo.Comun.DTOs.Clientes;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.WCF.Contratos
{
    /// <summary>
    /// Contrato de servicio para operaciones de clientes.
    /// </summary>
    [ServiceContract(Namespace = "http://bancoejemplo.com/servicios/clientes")]
    public interface IClientesServicio
    {
        [OperationContract]
        RespuestaOperacion<ClienteDTO> ConsultarClientePorIdentificacion(int tipoIdentificacionId, string numeroIdentificacion);

        [OperationContract]
        RespuestaOperacion<ClienteDTO> ConsultarClientePorId(int clienteId);

        [OperationContract]
        RespuestaOperacion<PaginacionDTO<ClienteDTO>> BuscarClientes(string nombres, string apellidos, string numeroIdentificacion, string estado, int? sucursalId, int pageNumber, int pageSize);

        [OperationContract]
        RespuestaOperacion<int> RegistrarCliente(ClienteDTO cliente, DireccionDTO direccion, ContactoDTO contacto, string usuario);
    }
}
""",
"Ejemplo.WCF.Contratos/ICuentasServicio.cs": r"""using System.Collections.Generic;
using System.ServiceModel;
using Ejemplo.Comun.DTOs.Cuentas;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.WCF.Contratos
{
    [ServiceContract(Namespace = "http://bancoejemplo.com/servicios/cuentas")]
    public interface ICuentasServicio
    {
        [OperationContract]
        RespuestaOperacion<List<CuentaDTO>> ConsultarCuentasCliente(int clienteId);

        [OperationContract]
        RespuestaOperacion<List<MovimientoCuentaDTO>> ConsultarMovimientosCuenta(int cuentaId, int pageNumber, int pageSize);

        [OperationContract]
        RespuestaOperacion<decimal> ConsultarSaldoCuenta(int cuentaId);
    }
}
""",
"Ejemplo.WCF.Contratos/ICreditosServicio.cs": r"""using System.Collections.Generic;
using System.ServiceModel;
using Ejemplo.Comun.DTOs.Creditos;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.WCF.Contratos
{
    [ServiceContract(Namespace = "http://bancoejemplo.com/servicios/creditos")]
    public interface ICreditosServicio
    {
        [OperationContract]
        RespuestaOperacion<OperacionCreditoDTO> ConsultarOperacionCredito(int operacionId);

        [OperationContract]
        RespuestaOperacion<SaldoOperacionDTO> ConsultarSaldosVigenteVencido(int operacionId);

        [OperationContract]
        RespuestaOperacion<List<CuotaCreditoDTO>> ConsultarCuotasOperacion(int operacionId);
    }
}
""",
"Ejemplo.WCF.Contratos/IPagosServicio.cs": r"""using System.Collections.Generic;
using System.ServiceModel;
using Ejemplo.Comun.DTOs.Pagos;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.WCF.Contratos
{
    [ServiceContract(Namespace = "http://bancoejemplo.com/servicios/pagos")]
    public interface IPagosServicio
    {
        [OperationContract]
        RespuestaOperacion<PagoRealizadoDTO> RegistrarPago(PagoDTO pago, string usuario);

        [OperationContract]
        RespuestaOperacion<List<PagoRealizadoDTO>> ConsultarPagosOperacion(int operacionId);
    }
}
""",
"Ejemplo.WCF.Contratos/ICarteraServicio.cs": r"""using System.ServiceModel;
using Ejemplo.Comun.DTOs.Cartera;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.WCF.Contratos
{
    [ServiceContract(Namespace = "http://bancoejemplo.com/servicios/cartera")]
    public interface ICarteraServicio
    {
        [OperationContract]
        RespuestaOperacion<ReporteCarteraDTO> ObtenerReporteCartera(int sucursalId);
    }
}
""",
"Ejemplo.WCF.Servicios/Ejemplo.WCF.Servicios.csproj": r"""<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{11111111-2222-3333-4444-555555555552}</ProjectGuid>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>Ejemplo.WCF.Servicios</RootNamespace>
    <AssemblyName>Ejemplo.WCF.Servicios</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="System" />
    <Reference Include="System.Core" />
    <Reference Include="System.ServiceModel" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="ClientesServicio.cs" />
    <Compile Include="CuentasServicio.cs" />
    <Compile Include="CreditosServicio.cs" />
    <Compile Include="PagosServicio.cs" />
    <Compile Include="CarteraServicio.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\Ejemplo.WCF.Contratos\Ejemplo.WCF.Contratos.csproj">
      <Project>{11111111-2222-3333-4444-555555555551}</Project>
      <Name>Ejemplo.WCF.Contratos</Name>
    </ProjectReference>
    <ProjectReference Include="..\..\Ejemplo.Comun\Ejemplo.Comun.DTOs\Ejemplo.Comun.DTOs.csproj">
      <Project>{00000000-0000-0000-0000-000000000001}</Project>
      <Name>Ejemplo.Comun.DTOs</Name>
    </ProjectReference>
    <ProjectReference Include="..\..\Ejemplo.Comun\Ejemplo.Comun.Utilidades\Ejemplo.Comun.Utilidades.csproj">
      <Project>{00000000-0000-0000-0000-000000000002}</Project>
      <Name>Ejemplo.Comun.Utilidades</Name>
    </ProjectReference>
    <ProjectReference Include="..\..\Ejemplo.Componentes\Ejemplo.Componentes.BLL\Ejemplo.Componentes.BLL.csproj">
      <Project>{00000000-0000-0000-0000-000000000004}</Project>
      <Name>Ejemplo.Componentes.BLL</Name>
    </ProjectReference>
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>""",
"Ejemplo.WCF.Servicios/Properties/AssemblyInfo.cs": r"""using System.Reflection;
using System.Runtime.InteropServices;
[assembly: AssemblyTitle("Ejemplo.WCF.Servicios")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Ejemplo.WCF.Servicios")]
[assembly: AssemblyCopyright("Copyright © 2024")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("11111111-2222-3333-4444-555555555552")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
""",
"Ejemplo.WCF.Servicios/ClientesServicio.cs": r"""using System;
using System.ServiceModel;
using Ejemplo.WCF.Contratos;
using Ejemplo.Comun.DTOs.Clientes;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Componentes.BLL;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.WCF.Servicios
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ClientesServicio : IClientesServicio
    {
        public RespuestaOperacion<ClienteDTO> ConsultarClientePorIdentificacion(int tipoIdentificacionId, string numeroIdentificacion)
        {
            try
            {
                LogUtil.Info($"ConsultarClientePorIdentificacion: tipo {tipoIdentificacionId}, num {numeroIdentificacion}");
                var bll = new ClientesBLL();
                var result = bll.ConsultarClientePorIdentificacion(tipoIdentificacionId, numeroIdentificacion);
                return RespuestaOperacion<ClienteDTO>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<ClienteDTO>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<ClienteDTO> ConsultarClientePorId(int clienteId)
        {
            try
            {
                LogUtil.Info($"ConsultarClientePorId: {clienteId}");
                var bll = new ClientesBLL();
                var result = bll.ConsultarClientePorId(clienteId);
                return RespuestaOperacion<ClienteDTO>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<ClienteDTO>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<PaginacionDTO<ClienteDTO>> BuscarClientes(string nombres, string apellidos, string numeroIdentificacion, string estado, int? sucursalId, int pageNumber, int pageSize)
        {
            try
            {
                LogUtil.Info($"BuscarClientes: page {pageNumber}");
                var bll = new ClientesBLL();
                var result = bll.BuscarClientes(nombres, apellidos, numeroIdentificacion, estado, sucursalId, pageNumber, pageSize);
                return RespuestaOperacion<PaginacionDTO<ClienteDTO>>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<PaginacionDTO<ClienteDTO>>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<int> RegistrarCliente(ClienteDTO cliente, DireccionDTO direccion, ContactoDTO contacto, string usuario)
        {
            try
            {
                LogUtil.Info($"RegistrarCliente: por {usuario}");
                var bll = new ClientesBLL();
                var result = bll.RegistrarCliente(cliente, direccion, contacto, usuario);
                return RespuestaOperacion<int>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<int>.CrearError(ex.Message);
            }
        }
    }
}
""",
"Ejemplo.WCF.Servicios/CuentasServicio.cs": r"""using System;
using System.Collections.Generic;
using System.ServiceModel;
using Ejemplo.WCF.Contratos;
using Ejemplo.Comun.DTOs.Cuentas;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Componentes.BLL;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.WCF.Servicios
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CuentasServicio : ICuentasServicio
    {
        public RespuestaOperacion<List<CuentaDTO>> ConsultarCuentasCliente(int clienteId)
        {
            try
            {
                LogUtil.Info($"ConsultarCuentasCliente: {clienteId}");
                var bll = new CuentasBLL();
                var result = bll.ConsultarCuentasCliente(clienteId);
                return RespuestaOperacion<List<CuentaDTO>>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<List<CuentaDTO>>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<List<MovimientoCuentaDTO>> ConsultarMovimientosCuenta(int cuentaId, int pageNumber, int pageSize)
        {
            try
            {
                LogUtil.Info($"ConsultarMovimientosCuenta: {cuentaId}");
                var bll = new CuentasBLL();
                var result = bll.ConsultarMovimientosCuenta(cuentaId, pageNumber, pageSize);
                return RespuestaOperacion<List<MovimientoCuentaDTO>>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<List<MovimientoCuentaDTO>>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<decimal> ConsultarSaldoCuenta(int cuentaId)
        {
            try
            {
                LogUtil.Info($"ConsultarSaldoCuenta: {cuentaId}");
                var bll = new CuentasBLL();
                var result = bll.ConsultarSaldoCuenta(cuentaId);
                return RespuestaOperacion<decimal>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<decimal>.CrearError(ex.Message);
            }
        }
    }
}
""",
"Ejemplo.WCF.Servicios/CreditosServicio.cs": r"""using System;
using System.Collections.Generic;
using System.ServiceModel;
using Ejemplo.WCF.Contratos;
using Ejemplo.Comun.DTOs.Creditos;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Componentes.BLL;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.WCF.Servicios
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CreditosServicio : ICreditosServicio
    {
        public RespuestaOperacion<OperacionCreditoDTO> ConsultarOperacionCredito(int operacionId)
        {
            try
            {
                LogUtil.Info($"ConsultarOperacionCredito: {operacionId}");
                var bll = new CreditosBLL();
                var result = bll.ConsultarOperacionCredito(operacionId);
                return RespuestaOperacion<OperacionCreditoDTO>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<OperacionCreditoDTO>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<SaldoOperacionDTO> ConsultarSaldosVigenteVencido(int operacionId)
        {
            try
            {
                LogUtil.Info($"ConsultarSaldosVigenteVencido: {operacionId}");
                var bll = new CreditosBLL();
                var result = bll.ConsultarSaldosVigenteVencido(operacionId);
                return RespuestaOperacion<SaldoOperacionDTO>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<SaldoOperacionDTO>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<List<CuotaCreditoDTO>> ConsultarCuotasOperacion(int operacionId)
        {
            try
            {
                LogUtil.Info($"ConsultarCuotasOperacion: {operacionId}");
                var bll = new CreditosBLL();
                var result = bll.ConsultarCuotasOperacion(operacionId);
                return RespuestaOperacion<List<CuotaCreditoDTO>>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<List<CuotaCreditoDTO>>.CrearError(ex.Message);
            }
        }
    }
}
""",
"Ejemplo.WCF.Servicios/PagosServicio.cs": r"""using System;
using System.Collections.Generic;
using System.ServiceModel;
using Ejemplo.WCF.Contratos;
using Ejemplo.Comun.DTOs.Pagos;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Componentes.BLL;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.WCF.Servicios
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class PagosServicio : IPagosServicio
    {
        public RespuestaOperacion<PagoRealizadoDTO> RegistrarPago(PagoDTO pago, string usuario)
        {
            try
            {
                LogUtil.Info($"RegistrarPago: operacion {pago?.OperacionId} por {usuario}");
                var bll = new PagosBLL();
                var result = bll.RegistrarPago(pago, usuario);
                return RespuestaOperacion<PagoRealizadoDTO>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<PagoRealizadoDTO>.CrearError(ex.Message);
            }
        }

        public RespuestaOperacion<List<PagoRealizadoDTO>> ConsultarPagosOperacion(int operacionId)
        {
            try
            {
                LogUtil.Info($"ConsultarPagosOperacion: {operacionId}");
                var bll = new PagosBLL();
                var result = bll.ConsultarPagosOperacion(operacionId);
                return RespuestaOperacion<List<PagoRealizadoDTO>>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<List<PagoRealizadoDTO>>.CrearError(ex.Message);
            }
        }
    }
}
""",
"Ejemplo.WCF.Servicios/CarteraServicio.cs": r"""using System;
using System.ServiceModel;
using Ejemplo.WCF.Contratos;
using Ejemplo.Comun.DTOs.Cartera;
using Ejemplo.Comun.DTOs.Comunes;
using Ejemplo.Componentes.BLL;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.WCF.Servicios
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CarteraServicio : ICarteraServicio
    {
        public RespuestaOperacion<ReporteCarteraDTO> ObtenerReporteCartera(int sucursalId)
        {
            try
            {
                LogUtil.Info($"ObtenerReporteCartera: sucursal {sucursalId}");
                var bll = new CarteraBLL();
                var result = bll.ObtenerReporteCartera(sucursalId);
                return RespuestaOperacion<ReporteCarteraDTO>.CrearExito(result);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RespuestaOperacion<ReporteCarteraDTO>.CrearError(ex.Message);
            }
        }
    }
}
""",
"Ejemplo.WCF.Host/Ejemplo.WCF.Host.csproj": r"""<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{11111111-2222-3333-4444-555555555553}</ProjectGuid>
    <OutputType>Exe</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>Ejemplo.WCF.Host</RootNamespace>
    <AssemblyName>Ejemplo.WCF.Host</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
    <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <PlatformTarget>AnyCPU</PlatformTarget>
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <PlatformTarget>AnyCPU</PlatformTarget>
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="System" />
    <Reference Include="System.Core" />
    <Reference Include="System.ServiceModel" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="Program.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <ItemGroup>
    <None Include="App.config" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\Ejemplo.WCF.Contratos\Ejemplo.WCF.Contratos.csproj">
      <Project>{11111111-2222-3333-4444-555555555551}</Project>
      <Name>Ejemplo.WCF.Contratos</Name>
    </ProjectReference>
    <ProjectReference Include="..\Ejemplo.WCF.Servicios\Ejemplo.WCF.Servicios.csproj">
      <Project>{11111111-2222-3333-4444-555555555552}</Project>
      <Name>Ejemplo.WCF.Servicios</Name>
    </ProjectReference>
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>""",
"Ejemplo.WCF.Host/Properties/AssemblyInfo.cs": r"""using System.Reflection;
using System.Runtime.InteropServices;
[assembly: AssemblyTitle("Ejemplo.WCF.Host")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Ejemplo.WCF.Host")]
[assembly: AssemblyCopyright("Copyright © 2024")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("11111111-2222-3333-4444-555555555553")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
""",
"Ejemplo.WCF.Host/App.config": r"""<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8" />
  </startup>
  <connectionStrings>
    <add name="BankLabDB" connectionString="Server=localhost;Database=BankLabDB;User Id=sa;Password=BankLab2024!;TrustServerCertificate=True;" providerName="System.Data.SqlClient" />
  </connectionStrings>
  <system.serviceModel>
    <bindings>
      <netTcpBinding>
        <binding name="TcpBindingConfig" maxReceivedMessageSize="2147483647" maxBufferSize="2147483647" sendTimeout="00:10:00" receiveTimeout="00:10:00">
          <security mode="None" />
          <readerQuotas maxDepth="2147483647" maxStringContentLength="2147483647" maxArrayLength="2147483647" maxBytesPerRead="2147483647" maxNameTableCharCount="2147483647" />
        </binding>
      </netTcpBinding>
    </bindings>
    <behaviors>
      <serviceBehaviors>
        <behavior name="ServiceBehaviorConfig">
          <serviceDebug includeExceptionDetailInFaults="true" />
          <serviceThrottling maxConcurrentCalls="1000" maxConcurrentSessions="1000" maxConcurrentInstances="1000" />
        </behavior>
      </serviceBehaviors>
    </behaviors>
    <services>
      <service name="Ejemplo.WCF.Servicios.ClientesServicio" behaviorConfiguration="ServiceBehaviorConfig">
        <endpoint address="net.tcp://localhost:8808/BancoEjemplo/Clientes" binding="netTcpBinding" bindingConfiguration="TcpBindingConfig" contract="Ejemplo.WCF.Contratos.IClientesServicio" />
      </service>
      <service name="Ejemplo.WCF.Servicios.CuentasServicio" behaviorConfiguration="ServiceBehaviorConfig">
        <endpoint address="net.tcp://localhost:8808/BancoEjemplo/Cuentas" binding="netTcpBinding" bindingConfiguration="TcpBindingConfig" contract="Ejemplo.WCF.Contratos.ICuentasServicio" />
      </service>
      <service name="Ejemplo.WCF.Servicios.CreditosServicio" behaviorConfiguration="ServiceBehaviorConfig">
        <endpoint address="net.tcp://localhost:8808/BancoEjemplo/Creditos" binding="netTcpBinding" bindingConfiguration="TcpBindingConfig" contract="Ejemplo.WCF.Contratos.ICreditosServicio" />
      </service>
      <service name="Ejemplo.WCF.Servicios.PagosServicio" behaviorConfiguration="ServiceBehaviorConfig">
        <endpoint address="net.tcp://localhost:8808/BancoEjemplo/Pagos" binding="netTcpBinding" bindingConfiguration="TcpBindingConfig" contract="Ejemplo.WCF.Contratos.IPagosServicio" />
      </service>
      <service name="Ejemplo.WCF.Servicios.CarteraServicio" behaviorConfiguration="ServiceBehaviorConfig">
        <endpoint address="net.tcp://localhost:8808/BancoEjemplo/Cartera" binding="netTcpBinding" bindingConfiguration="TcpBindingConfig" contract="Ejemplo.WCF.Contratos.ICarteraServicio" />
      </service>
    </services>
  </system.serviceModel>
</configuration>
""",
"Ejemplo.WCF.Host/Program.cs": r"""using System;
using System.ServiceModel;
using Ejemplo.WCF.Servicios;

namespace Ejemplo.WCF.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost hostClientes = null;
            ServiceHost hostCuentas = null;
            ServiceHost hostCreditos = null;
            ServiceHost hostPagos = null;
            ServiceHost hostCartera = null;

            try
            {
                hostClientes = new ServiceHost(typeof(ClientesServicio));
                hostCuentas = new ServiceHost(typeof(CuentasServicio));
                hostCreditos = new ServiceHost(typeof(CreditosServicio));
                hostPagos = new ServiceHost(typeof(PagosServicio));
                hostCartera = new ServiceHost(typeof(CarteraServicio));

                hostClientes.Open();
                Console.WriteLine("Servicio Clientes iniciado en " + hostClientes.Description.Endpoints[0].Address);

                hostCuentas.Open();
                Console.WriteLine("Servicio Cuentas iniciado en " + hostCuentas.Description.Endpoints[0].Address);

                hostCreditos.Open();
                Console.WriteLine("Servicio Creditos iniciado en " + hostCreditos.Description.Endpoints[0].Address);

                hostPagos.Open();
                Console.WriteLine("Servicio Pagos iniciado en " + hostPagos.Description.Endpoints[0].Address);

                hostCartera.Open();
                Console.WriteLine("Servicio Cartera iniciado en " + hostCartera.Description.Endpoints[0].Address);

                Console.WriteLine();
                Console.WriteLine("Todos los servicios WCF de Banco Ejemplo iniciados exitosamente.");
                Console.WriteLine("Presione ENTER para detener...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al iniciar los servicios:");
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
            finally
            {
                if (hostClientes?.State == CommunicationState.Opened) hostClientes.Close();
                if (hostCuentas?.State == CommunicationState.Opened) hostCuentas.Close();
                if (hostCreditos?.State == CommunicationState.Opened) hostCreditos.Close();
                if (hostPagos?.State == CommunicationState.Opened) hostPagos.Close();
                if (hostCartera?.State == CommunicationState.Opened) hostCartera.Close();
            }
        }
    }
}
""",
"Ejemplo.WCF.Agentes/Ejemplo.WCF.Agentes.csproj": r"""<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{11111111-2222-3333-4444-555555555554}</ProjectGuid>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>Ejemplo.WCF.Agentes</RootNamespace>
    <AssemblyName>Ejemplo.WCF.Agentes</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="System" />
    <Reference Include="System.Core" />
    <Reference Include="System.Configuration" />
    <Reference Include="System.ServiceModel" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="AgenteBase.cs" />
    <Compile Include="ClientesAgente.cs" />
    <Compile Include="CuentasAgente.cs" />
    <Compile Include="CreditosAgente.cs" />
    <Compile Include="PagosAgente.cs" />
    <Compile Include="CarteraAgente.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\Ejemplo.WCF.Contratos\Ejemplo.WCF.Contratos.csproj">
      <Project>{11111111-2222-3333-4444-555555555551}</Project>
      <Name>Ejemplo.WCF.Contratos</Name>
    </ProjectReference>
    <ProjectReference Include="..\..\Ejemplo.Comun\Ejemplo.Comun.DTOs\Ejemplo.Comun.DTOs.csproj">
      <Project>{00000000-0000-0000-0000-000000000001}</Project>
      <Name>Ejemplo.Comun.DTOs</Name>
    </ProjectReference>
    <ProjectReference Include="..\..\Ejemplo.Comun\Ejemplo.Comun.Utilidades\Ejemplo.Comun.Utilidades.csproj">
      <Project>{00000000-0000-0000-0000-000000000002}</Project>
      <Name>Ejemplo.Comun.Utilidades</Name>
    </ProjectReference>
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>""",
"Ejemplo.WCF.Agentes/Properties/AssemblyInfo.cs": r"""using System.Reflection;
using System.Runtime.InteropServices;
[assembly: AssemblyTitle("Ejemplo.WCF.Agentes")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Ejemplo.WCF.Agentes")]
[assembly: AssemblyCopyright("Copyright © 2024")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("11111111-2222-3333-4444-555555555554")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
""",
"Ejemplo.WCF.Agentes/AgenteBase.cs": r"""using System;
using System.ServiceModel;
using Ejemplo.Comun.Utilidades;

namespace Ejemplo.WCF.Agentes
{
    public abstract class AgenteBase<TContrato> : IDisposable where TContrato : class
    {
        private readonly string _endpointAddress;
        private ChannelFactory<TContrato> _factory;
        private TContrato _canal;
        private readonly object _lockObj = new object();

        protected AgenteBase(string endpointAddress)
        {
            _endpointAddress = endpointAddress;
        }

        protected TContrato Canal
        {
            get
            {
                lock (_lockObj)
                {
                    if (_factory == null || _factory.State == CommunicationState.Faulted)
                    {
                        var binding = new NetTcpBinding(SecurityMode.None)
                        {
                            MaxReceivedMessageSize = 2147483647,
                            MaxBufferSize = 2147483647,
                            SendTimeout = TimeSpan.FromMinutes(10),
                            ReceiveTimeout = TimeSpan.FromMinutes(10)
                        };
                        var endpoint = new EndpointAddress(_endpointAddress);
                        _factory = new ChannelFactory<TContrato>(binding, endpoint);
                    }

                    if (_canal == null || ((ICommunicationObject)_canal).State == CommunicationState.Faulted)
                    {
                        _canal = _factory.CreateChannel();
                    }

                    return _canal;
                }
            }
        }

        protected TResult EjecutarOperacion<TResult>(Func<TContrato, TResult> operacion, string nombreOperacion)
        {
            try
            {
                LogUtil.Info($"Ejecutando operación WCF: {nombreOperacion} en {_endpointAddress}");
                return operacion(Canal);
            }
            catch (CommunicationException ce)
            {
                LogUtil.Error($"Error de comunicación en {nombreOperacion}: {ce.Message}");
                // Reintentar una vez
                lock (_lockObj)
                {
                    if (_canal != null)
                    {
                        ((ICommunicationObject)_canal).Abort();
                        _canal = null;
                    }
                }
                return operacion(Canal);
            }
            catch (TimeoutException te)
            {
                LogUtil.Error($"Timeout en {nombreOperacion}: {te.Message}");
                throw;
            }
            catch (Exception ex)
            {
                LogUtil.Error($"Error general en {nombreOperacion}: {ex.Message}");
                throw;
            }
        }

        public void Dispose()
        {
            if (_canal != null)
            {
                var canalComm = _canal as ICommunicationObject;
                if (canalComm.State == CommunicationState.Opened)
                {
                    canalComm.Close();
                }
                else
                {
                    canalComm.Abort();
                }
            }

            if (_factory != null)
            {
                if (_factory.State == CommunicationState.Opened)
                {
                    _factory.Close();
                }
                else
                {
                    _factory.Abort();
                }
            }
        }
    }
}
""",
"Ejemplo.WCF.Agentes/ClientesAgente.cs": r"""using System.Configuration;
using Ejemplo.WCF.Contratos;
using Ejemplo.Comun.DTOs.Clientes;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.WCF.Agentes
{
    public class ClientesAgente : AgenteBase<IClientesServicio>, IClientesServicio
    {
        public ClientesAgente() 
            : base(ConfigurationManager.AppSettings["WCF_Endpoint_Clientes"] ?? "net.tcp://localhost:8808/BancoEjemplo/Clientes")
        {
        }

        public RespuestaOperacion<ClienteDTO> ConsultarClientePorIdentificacion(int tipoIdentificacionId, string numeroIdentificacion)
        {
            return EjecutarOperacion(c => c.ConsultarClientePorIdentificacion(tipoIdentificacionId, numeroIdentificacion), "ConsultarClientePorIdentificacion");
        }

        public RespuestaOperacion<ClienteDTO> ConsultarClientePorId(int clienteId)
        {
            return EjecutarOperacion(c => c.ConsultarClientePorId(clienteId), "ConsultarClientePorId");
        }

        public RespuestaOperacion<PaginacionDTO<ClienteDTO>> BuscarClientes(string nombres, string apellidos, string numeroIdentificacion, string estado, int? sucursalId, int pageNumber, int pageSize)
        {
            return EjecutarOperacion(c => c.BuscarClientes(nombres, apellidos, numeroIdentificacion, estado, sucursalId, pageNumber, pageSize), "BuscarClientes");
        }

        public RespuestaOperacion<int> RegistrarCliente(ClienteDTO cliente, DireccionDTO direccion, ContactoDTO contacto, string usuario)
        {
            return EjecutarOperacion(c => c.RegistrarCliente(cliente, direccion, contacto, usuario), "RegistrarCliente");
        }
    }
}
""",
"Ejemplo.WCF.Agentes/CuentasAgente.cs": r"""using System.Collections.Generic;
using System.Configuration;
using Ejemplo.WCF.Contratos;
using Ejemplo.Comun.DTOs.Cuentas;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.WCF.Agentes
{
    public class CuentasAgente : AgenteBase<ICuentasServicio>, ICuentasServicio
    {
        public CuentasAgente() 
            : base(ConfigurationManager.AppSettings["WCF_Endpoint_Cuentas"] ?? "net.tcp://localhost:8808/BancoEjemplo/Cuentas")
        {
        }

        public RespuestaOperacion<List<CuentaDTO>> ConsultarCuentasCliente(int clienteId)
        {
            return EjecutarOperacion(c => c.ConsultarCuentasCliente(clienteId), "ConsultarCuentasCliente");
        }

        public RespuestaOperacion<List<MovimientoCuentaDTO>> ConsultarMovimientosCuenta(int cuentaId, int pageNumber, int pageSize)
        {
            return EjecutarOperacion(c => c.ConsultarMovimientosCuenta(cuentaId, pageNumber, pageSize), "ConsultarMovimientosCuenta");
        }

        public RespuestaOperacion<decimal> ConsultarSaldoCuenta(int cuentaId)
        {
            return EjecutarOperacion(c => c.ConsultarSaldoCuenta(cuentaId), "ConsultarSaldoCuenta");
        }
    }
}
""",
"Ejemplo.WCF.Agentes/CreditosAgente.cs": r"""using System.Collections.Generic;
using System.Configuration;
using Ejemplo.WCF.Contratos;
using Ejemplo.Comun.DTOs.Creditos;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.WCF.Agentes
{
    public class CreditosAgente : AgenteBase<ICreditosServicio>, ICreditosServicio
    {
        public CreditosAgente() 
            : base(ConfigurationManager.AppSettings["WCF_Endpoint_Creditos"] ?? "net.tcp://localhost:8808/BancoEjemplo/Creditos")
        {
        }

        public RespuestaOperacion<OperacionCreditoDTO> ConsultarOperacionCredito(int operacionId)
        {
            return EjecutarOperacion(c => c.ConsultarOperacionCredito(operacionId), "ConsultarOperacionCredito");
        }

        public RespuestaOperacion<SaldoOperacionDTO> ConsultarSaldosVigenteVencido(int operacionId)
        {
            return EjecutarOperacion(c => c.ConsultarSaldosVigenteVencido(operacionId), "ConsultarSaldosVigenteVencido");
        }

        public RespuestaOperacion<List<CuotaCreditoDTO>> ConsultarCuotasOperacion(int operacionId)
        {
            return EjecutarOperacion(c => c.ConsultarCuotasOperacion(operacionId), "ConsultarCuotasOperacion");
        }
    }
}
""",
"Ejemplo.WCF.Agentes/PagosAgente.cs": r"""using System.Collections.Generic;
using System.Configuration;
using Ejemplo.WCF.Contratos;
using Ejemplo.Comun.DTOs.Pagos;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.WCF.Agentes
{
    public class PagosAgente : AgenteBase<IPagosServicio>, IPagosServicio
    {
        public PagosAgente() 
            : base(ConfigurationManager.AppSettings["WCF_Endpoint_Pagos"] ?? "net.tcp://localhost:8808/BancoEjemplo/Pagos")
        {
        }

        public RespuestaOperacion<PagoRealizadoDTO> RegistrarPago(PagoDTO pago, string usuario)
        {
            return EjecutarOperacion(c => c.RegistrarPago(pago, usuario), "RegistrarPago");
        }

        public RespuestaOperacion<List<PagoRealizadoDTO>> ConsultarPagosOperacion(int operacionId)
        {
            return EjecutarOperacion(c => c.ConsultarPagosOperacion(operacionId), "ConsultarPagosOperacion");
        }
    }
}
""",
"Ejemplo.WCF.Agentes/CarteraAgente.cs": r"""using System.Configuration;
using Ejemplo.WCF.Contratos;
using Ejemplo.Comun.DTOs.Cartera;
using Ejemplo.Comun.DTOs.Comunes;

namespace Ejemplo.WCF.Agentes
{
    public class CarteraAgente : AgenteBase<ICarteraServicio>, ICarteraServicio
    {
        public CarteraAgente() 
            : base(ConfigurationManager.AppSettings["WCF_Endpoint_Cartera"] ?? "net.tcp://localhost:8808/BancoEjemplo/Cartera")
        {
        }

        public RespuestaOperacion<ReporteCarteraDTO> ObtenerReporteCartera(int sucursalId)
        {
            return EjecutarOperacion(c => c.ObtenerReporteCartera(sucursalId), "ObtenerReporteCartera");
        }
    }
}
"""
}

for rel_path, content in files.items():
    full_path = os.path.join(base_path, rel_path)
    os.makedirs(os.path.dirname(full_path), exist_ok=True)
    with open(full_path, "w", encoding="utf-8") as f:
        f.write(content.strip() + "\n")

print("Files generated successfully.")
