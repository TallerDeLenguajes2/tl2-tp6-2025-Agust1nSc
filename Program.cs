using Microsoft.Data.Sqlite;
string connectionString = "Data Source=Tienda.db;";

// Crear conexión a la base de datos
using (SqliteConnection connection = new SqliteConnection(connectionString))
{
    connection.Open();


    string createProductos = @"CREATE TABLE IF NOT EXISTS Productos (idProducto INTEGER PRIMARY KEY AUTOINCREMENT, Descripcion TEXT(100), Precio REAL);";
    new SqliteCommand(createProductos, connection).ExecuteNonQuery();

    string createPresupuestos = @"CREATE TABLE IF NOT EXISTS Presupuestos (idPresupuesto INTEGER PRIMARY KEY AUTOINCREMENT, NombreDestinatario TEXT(100), FechaCreacion TEXT);";
    new SqliteCommand(createPresupuestos, connection).ExecuteNonQuery();

    string createPresupuestosDetalle = @"CREATE TABLE IF NOT EXISTS PresupuestosDetalle (idDetalle INTEGER PRIMARY KEY AUTOINCREMENT, idPresupuesto INTEGER, idProducto INTEGER, Cantidad INTEGER, FOREIGN KEY (idPresupuesto) REFERENCES Presupuestos(idPresupuesto), FOREIGN KEY (idProducto) REFERENCES Productos(idProducto));";
    new SqliteCommand(createPresupuestosDetalle, connection).ExecuteNonQuery();

    Console.WriteLine("Tablas creadas correctamente.\n");


    string insertQuery = "INSERT INTO productos (Descripcion, Precio) VALUES ('Pera', 0.80), ('Uva', 0.20)";
    using (SqliteCommand insertCmd = new SqliteCommand(insertQuery, connection))
    {
        insertCmd.ExecuteNonQuery();
        Console.WriteLine("Datos insertados en la tabla 'productos'.");
    }

    string insertQuery2 = "INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES ('Carlos Gonzales', '2024-10-24')";
    using (SqliteCommand insertCmd = new SqliteCommand(insertQuery2, connection))
    {
        insertCmd.ExecuteNonQuery();
        Console.WriteLine("Datos insertados en la tabla 'productos'.");
    }
    string insertQuery3 = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES (2, 1, 3); ";
    using (SqliteCommand insertCmd = new SqliteCommand(insertQuery3, connection))
    {
        insertCmd.ExecuteNonQuery();
        Console.WriteLine("Datos insertados en la tabla 'productos'.");
    }

    string updateProducto = @" UPDATE Productos SET Descripcion = 'Teclado Mecánico Corsair', Precio = 122000 WHERE idProducto = 2";

    new SqliteCommand(updateProducto, connection).ExecuteNonQuery();
    Console.WriteLine("Producto modificado.");

    string updateProducto2 = @" UPDATE Presupuestos SET NombreDestinatario = 'Luis Gonzales' WHERE idPresupuesto = 1";

    new SqliteCommand(updateProducto, connection).ExecuteNonQuery();
    Console.WriteLine("Nombre de destinatario modificado.");

    string deleteDetalle = "DELETE FROM PresupuestosDetalle WHERE idPresupuesto = 1 AND idProducto = 1;";
    new SqliteCommand(deleteDetalle, connection).ExecuteNonQuery();
    Console.WriteLine("Registro eliminado de PresupuestosDetalle.\n");

    // Leer datos
    string selectQuery = "SELECT * FROM productos";
    using (SqliteCommand selectCmd = new SqliteCommand(selectQuery, connection))
    using (SqliteDataReader reader = selectCmd.ExecuteReader())
    {
        Console.WriteLine("Datos en la tabla 'productos':");
        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["idProducto"]}, Descripcion: {reader["Descripcion"]}, Precio: {reader["Precio"]}");
        }
    }

    Console.WriteLine("=== Presupuestos ===");
    using (var cmd = new SqliteCommand("SELECT * FROM Presupuestos;", connection))
    using (var reader = cmd.ExecuteReader())
    {
        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["idPresupuesto"]}, Destinatario: {reader["NombreDestinatario"]}, Fecha: {reader["FechaCreacion"]}");
        }
    }

    Console.WriteLine("=== PresupuestosDetalle ===");
    using (var cmd = new SqliteCommand("SELECT * FROM PresupuestosDetalle;", connection))
    using (var reader = cmd.ExecuteReader())
    {
        while (reader.Read())
        {
            Console.WriteLine($"idPresupuesto: {reader["idPresupuesto"]}, idProducto: {reader["idProducto"]}, Cantidad: {reader["Cantidad"]}");
        }
    }

    connection.Close();
}