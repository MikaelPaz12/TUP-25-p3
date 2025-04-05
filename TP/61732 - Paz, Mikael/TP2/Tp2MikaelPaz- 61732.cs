using System;
using System.Collections.Generic;

// Clase Banco
class Banco
{
    public string Nombre { get; private set; }
    public List<Cliente> Clientes { get; private set; }
    public List<Operacion> Operaciones { get; private set; }

    public Banco(string nombre)
    {
        Nombre = nombre;
        Clientes = new List<Cliente>();
        Operaciones = new List<Operacion>();
    }

    public void Agregar(Cliente cliente)
    {
        Clientes.Add(cliente);
    }

    public void Registrar(Operacion operacion)
    {
        Operaciones.Add(operacion);
        operacion.Ejecutar(this);
    }

    public Cliente BuscarClientePorCuenta(string numeroCuenta)
    {
        foreach (var cliente in Clientes)
        {
            if (cliente.BuscarCuenta(numeroCuenta) != null)
                return cliente;
        }
        return null;
    }

    public void Informe()
    {
        Console.WriteLine($"\nBanco: {Nombre} | Clientes: {Clientes.Count}\n");
        foreach (var cliente in Clientes)
        {
            cliente.Informe();
        }
    }
}

// Clase Cliente
class Cliente
{
    public string Nombre { get; private set; }
    public List<Cuenta> Cuentas { get; private set; }
    public List<Operacion> HistorialOperaciones { get; private set; }

    public Cliente(string nombre)
    {
        Nombre = nombre;
        Cuentas = new List<Cuenta>();
        HistorialOperaciones = new List<Operacion>();
    }

    public void Agregar(Cuenta cuenta)
    {
        Cuentas.Add(cuenta);
    }

    public Cuenta BuscarCuenta(string numeroCuenta)
    {
        return Cuentas.Find(c => c.Numero == numeroCuenta);
    }

    public void RegistrarOperacion(Operacion operacion)
    {
        HistorialOperaciones.Add(operacion);
    }

    public void Informe()
    {
        double saldoTotal = 0;
        double puntosTotal = 0;

        Console.WriteLine($"  Cliente: {Nombre}\n");
        foreach (var cuenta in Cuentas)
        {
            saldoTotal += cuenta.Saldo;
            puntosTotal += cuenta.Puntos;
            cuenta.Informe();
        }

        Console.WriteLine($"  Saldo Total: ${saldoTotal:F2} | Puntos Total: ${puntosTotal:F2}\n");
    }
}

// Clase abstracta Cuenta
abstract class Cuenta
{
    public string Numero { get; private set; }
    public double Saldo { get; protected set; }
    public double Puntos { get; protected set; }

    protected Cuenta(string numero, double saldoInicial)
    {
        Numero = numero;
        Saldo = saldoInicial;
        Puntos = 0;
    }

    public abstract void AcumularPuntos(double monto);

    public void Depositar(double monto)
    {
        Saldo += monto;
    }

    public bool Extraer(double monto)
    {
        if (Saldo >= monto)
        {
            Saldo -= monto;
            return true;
        }
        return false;
    }

    public void Informe()
    {
        Console.WriteLine($"    Cuenta: {Numero} | Saldo: ${Saldo:F2} | Puntos: ${Puntos:F2}\n");
    }
}

// Clases específicas de Cuenta
class CuentaOro : Cuenta
{
    public CuentaOro(string numero, double saldoInicial) : base(numero, saldoInicial) { }

    public override void AcumularPuntos(double monto)
    {
        Puntos += monto > 1000 ? monto * 0.05 : monto * 0.03;
    }
}

class CuentaPlata : Cuenta
{
    public CuentaPlata(string numero, double saldoInicial) : base(numero, saldoInicial) { }

    public override void AcumularPuntos(double monto)
    {
        Puntos += monto * 0.02;
    }
}

class CuentaBronce : Cuenta
{
    public CuentaBronce(string numero, double saldoInicial) : base(numero, saldoInicial) { }

    public override void AcumularPuntos(double monto)
    {
        Puntos += monto * 0.01;
    }
}

// Clase abstracta Operacion
abstract class Operacion
{
    public string Tipo { get; protected set; }
    public double Monto { get; protected set; }
    public string CuentaOrigen { get; protected set; }
    public string CuentaDestino { get; protected set; }

    protected Operacion(string tipo, double monto, string cuentaOrigen, string cuentaDestino = null)
    {
        Tipo = tipo;
        Monto = monto;
        CuentaOrigen = cuentaOrigen;
        CuentaDestino = cuentaDestino;
    }

    public abstract void Ejecutar(Banco banco);
}

// Clases específicas de Operaciones
class Deposito : Operacion
{
    public Deposito(string cuentaOrigen, double monto) : base("Deposito", monto, cuentaOrigen) { }

    public override void Ejecutar(Banco banco)
    {
        var cliente = banco.BuscarClientePorCuenta(CuentaOrigen);
        var cuenta = cliente?.BuscarCuenta(CuentaOrigen);
        if (cuenta != null)
        {
            cuenta.Depositar(Monto);
            cliente.RegistrarOperacion(this);
        }
    }
}

class Retiro : Operacion
{
    public Retiro(string cuentaOrigen, double monto) : base("Retiro", monto, cuentaOrigen) { }

    public override void Ejecutar(Banco banco)
    {
        var cliente = banco.BuscarClientePorCuenta(CuentaOrigen);
        var cuenta = cliente?.BuscarCuenta(CuentaOrigen);
        if (cuenta != null && cuenta.Extraer(Monto))
        {
            cliente.RegistrarOperacion(this);
        }
    }
}

class Pago : Operacion
{
    public Pago(string cuentaOrigen, double monto) : base("Pago", monto, cuentaOrigen) { }

    public override void Ejecutar(Banco banco)
    {
        var cliente = banco.BuscarClientePorCuenta(CuentaOrigen);
        var cuenta = cliente?.BuscarCuenta(CuentaOrigen);
        if (cuenta != null && cuenta.Extraer(Monto))
        {
            cuenta.AcumularPuntos(Monto);
            cliente.RegistrarOperacion(this);
        }
    }
}

class Transferencia : Operacion
{
    public Transferencia(string cuentaOrigen, string cuentaDestino, double monto)
        : base("Transferencia", monto, cuentaOrigen, cuentaDestino) { }

    public override void Ejecutar(Banco banco)
    {
        var clienteOrigen = banco.BuscarClientePorCuenta(CuentaOrigen);
        var clienteDestino = banco.BuscarClientePorCuenta(CuentaDestino);
        var cuentaOrigen = clienteOrigen?.BuscarCuenta(CuentaOrigen);
        var cuentaDestino = clienteDestino?.BuscarCuenta(CuentaDestino);

        if (cuentaOrigen != null && cuentaDestino != null && cuentaOrigen.Extraer(Monto))
        {
            cuentaDestino.Depositar(Monto);
            clienteOrigen.RegistrarOperacion(this);
            clienteDestino.RegistrarOperacion(this);
        }
    }
}

// Programa principal para pruebas
class Program
{
    static void Main()
    {
        var raul = new Cliente("Raul Perez");
        raul.Agregar(new CuentaOro("10001", 1000));
        raul.Agregar(new CuentaPlata("10002", 2000));

        var sara = new Cliente("Sara Lopez");
        sara.Agregar(new CuentaPlata("10003", 3000));
        sara.Agregar(new CuentaPlata("10004", 4000));

        var luis = new Cliente("Luis Gomez");
        luis.Agregar(new CuentaBronce("10005", 5000));

        var nac = new Banco("Banco Nac");
        nac.Agregar(raul);
        nac.Agregar(sara);

        var tup = new Banco("Banco TUP");
        tup.Agregar(luis);

        nac.Registrar(new Deposito("10001", 100));
        nac.Registrar(new Retiro("10002", 200));
        nac.Registrar(new Transferencia("10001", "10002", 300));
        nac.Registrar(new Transferencia("10003", "10004", 500));
        nac.Registrar(new Pago("10002", 400));

        tup.Registrar(new Deposito("10005", 100));
        tup.Registrar(new Retiro("10005", 200));
        tup.Registrar(new Transferencia("10005", "10002", 300));
        tup.Registrar(new Pago("10005", 400));

        nac.Informe();
        tup.Informe();
    }
}

