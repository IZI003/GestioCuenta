using AccesoDatos;
using Comunes.Respuesta;
using Dapper;
using Cuenta.Modelos;

namespace Cuenta.Servicios;

public class Cuentas : ICuenta
{
    private BD bd;
    public Cuentas(BD bd)
    {
        this.bd = bd;
    }

    public async Task<SalidaSaldo> ObtenerSaldoIdCuenta(int idCuenta)
    {
        var salida = new SalidaSaldo();
        try
        {
            var query = @"select saldo,id_cuenta as cuenta 
                        
                        from cuenta where id_cuenta = @idCuenta";

            using var con = bd.ObtenerConexion();
            var datosDb = await con.QueryAsync<Saldo>(query, new { idCuenta });

            salida.saldo = datosDb.FirstOrDefault();
        }
        catch (Exception ex)
        {
            salida.RespuestaBD = new RespuestaBD($"ERROR|Error al consultar los datos. {ex.Message}");
        }

        salida.RespuestaBD = new RespuestaBD("OK");
        return salida;
    }

    public async Task<SalidaSaldoTotal> ObtenerSaldoIdUsuario(int idUsuario)
    {
        var salida = new SalidaSaldoTotal();
        try
        {
            var query = "select SUM(saldo) as saldo from cuenta where id_usuario = @idUsuario";

            using var con = bd.ObtenerConexion();
            var datosDb = await con.QueryAsync<decimal>(query, new { idUsuario });

            salida.saldo = datosDb.FirstOrDefault();
        }
        catch (Exception ex)
        {
            salida.RespuestaBD = new RespuestaBD($"ERROR|Error al consultar los datos. {ex.Message}");
        }

        salida.RespuestaBD = new RespuestaBD("OK");
        return salida;
    }

    public async Task<RespuestaBD> CrearCuenta(int idUsuario)
    {
        var salida = new RespuestaBD();
        try
        {
            using var con = bd.ObtenerConexion();
            await con.QueryAsync($"Insert into cuenta (saldo,id_usuario) values (0,{idUsuario})");
            salida = new RespuestaBD("OK");
        }
        catch (Exception ex)
        {
            salida = new RespuestaBD($"ERROR|Error al Crear la cuenta. {ex.Message}");
        }

        return salida;
    }
}