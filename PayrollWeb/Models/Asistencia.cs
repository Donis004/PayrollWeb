﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace PayrollWeb.Models
{
    public class Asistencia
    {
        public int IdAsistencia { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime? Fecha { get; set; }      
        public TimeSpan? HoraEntrada { get; set; } 
        public TimeSpan? HoraSalida { get; set; } 
        public string? Ausencia { get; set; }

        Conexion conexion = new Conexion();


        // Métodos:


        //Con este método podrá ver todas las asistencias de un empleado
        public List<string> VerAsistencia(string dui)
        {
            List<string> Asistencia = new List<string>();
            string query = "SELECT Asistencia.fecha, Asistencia.hora_entrada, Asistencia.hora_salida FROM Asistencia JOIN Empleado on Asistencia.id_empleado = Empleado.id_empleado WHERE Empleado.dui = @dui";

            using (SqlConnection connection = conexion.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@dui", dui);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string fecha = reader.GetDateTime(0).ToString("yyyy-MM-dd");
                                string horaEntrada = reader.GetTimeSpan(1).ToString(@"hh\:mm");
                                string horaSalida = reader.GetTimeSpan(2).ToString(@"hh\:mm");
                                Asistencia.Add($"{fecha}|{horaEntrada}|{horaSalida}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener la información: " + ex.Message);
                }
            }
            return Asistencia;
        }


        ////Con este podrá ver la asistencia de un día específico de un empleado
        //public List<string> ObtenerAsistenciaPorFecha(DateTime fecha, string dui)
        //{
        //    List<string> Asistencia = new List<string>();

        //    // Consulta SQL ajustada
        //    string query = "SELECT Asistencia.fecha, Asistencia.hora_entrada, Asistencia.hora_salida " +
        //                   "FROM Asistencia " +
        //                   "JOIN Empleado ON Asistencia.id_empleado = Empleado.id_empleado " +
        //                   "WHERE Empleado.dui = @dui AND Asistencia.fecha = @fecha";

        //    // Crear la conexión y comando
        //    using (SqlConnection connection = conexion.GetConnection())
        //    {
        //        try
        //        {
        //            connection.Open();

        //            // Preparar el comando SQL
        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                // Agregar parámetros correctamente
        //                command.Parameters.AddWithValue("@dui", dui);
        //                command.Parameters.AddWithValue("@fecha", fecha.Date); // Usar .Date para comparar solo la fecha

        //                // Ejecutar la consulta y leer los resultados
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    // Leer los resultados
        //                    while (reader.Read())
        //                    {
        //                        string fechaAsistencia = reader.GetDateTime(0).ToString("yyyy-MM-dd"); // Convertir a formato de fecha
        //                        string horaEntrada = reader.GetTimeSpan(1).ToString(@"hh\:mm"); // Convertir a formato de hora
        //                        string horaSalida = reader.GetTimeSpan(2).ToString(@"hh\:mm"); // Convertir a formato de hora

        //                        // Agregar los datos a la lista como un string formateado
        //                        Asistencia.Add($"{fechaAsistencia}|{horaEntrada}|{horaSalida}");
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Manejo de errores
        //            Console.WriteLine("Error al obtener la información: " + ex.Message);

        //        }
        //    }


        //    // Retornar la lista con los datos obtenidos
        //    return Asistencia;
        //}


        //public List<string> VerAsistencia()
        //{
        //    List<string> Asistencia = new List<string>();
        //    string query = "SELECT Asistencia.fecha, Empleado.id_empleado, Empleado.nombre, Empleado.apellidos, Asistencia.hora_entrada, Asistencia.hora_salida " +
        //                   "FROM Asistencia " +
        //                   "JOIN Empleado ON Asistencia.id_empleado = Empleado.id_empleado";

        //    using (SqlConnection connection = conexion.GetConnection())
        //    {
        //        try
        //        {
        //            connection.Open();
        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        string fechaAsistencia = reader.GetDateTime(0).ToString("yyyy-MM-dd");
        //                        string idEmpleado = reader.GetInt32(1).ToString();
        //                        string nombreEmpleado = reader.GetString(2);
        //                        string apellidoEmpleado = reader.GetString(3);
        //                        string horaEntrada = reader.GetTimeSpan(4).ToString(@"hh\:mm");
        //                        string horaSalida = reader.GetTimeSpan(5).ToString(@"hh\:mm");

        //                        // Concatenamos los datos de la asistencia, incluyendo fecha, ID, nombre, apellido,
        //                        // hora de entrada y hora de salida.
        //                        Asistencia.Add($"{fechaAsistencia}|{idEmpleado}|{nombreEmpleado}|{apellidoEmpleado}|{horaEntrada}|{horaSalida}");
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Error al obtener la información: " + ex.Message);
        //        }
        //    }
        //    return Asistencia;
        //}

        public List<string> VerAsistencia()
        {
            List<string> Asistencia = new List<string>();
            string query = "SELECT Asistencia.fecha, Empleado.id_empleado, Empleado.nombre, Empleado.apellidos, Asistencia.hora_entrada, Asistencia.hora_salida " +
                           "FROM Asistencia " +
                           "JOIN Empleado ON Asistencia.id_empleado = Empleado.id_empleado";

            using (SqlConnection connection = conexion.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string fechaAsistencia = reader.GetDateTime(0).ToString("yyyy-MM-dd");
                                string idEmpleado = reader.GetInt32(1).ToString();
                                string nombreEmpleado = reader.GetString(2);
                                string apellidoEmpleado = reader.GetString(3);
                                string horaEntrada =  reader.GetTimeSpan(4).ToString(@"hh\:mm");
                                string horaSalida = reader.GetTimeSpan(5).ToString(@"hh\:mm");

                                if(horaEntrada == "00:00")
                                {
                                    horaEntrada = "--:--";
                                }

                                if (horaSalida == "00:00")
                                {
                                    horaSalida = "--:--";
                                }

                                // Agregar la información a la lista
                                Asistencia.Add($"{fechaAsistencia}|{idEmpleado}|{nombreEmpleado}|{apellidoEmpleado}|{horaEntrada}|{horaSalida}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener la información: " + ex.Message);
                }
            }
            return Asistencia;
        }




        public List<string> ObtenerAsistenciaPorEmpleadoYFecha(int id, string fecha)
        {
            List<string> listaAsistenciaMostrar = new List<string>();
            string query = "SELECT Asistencia.fecha, Empleado.id_empleado, Empleado.nombre, Empleado.apellidos, Asistencia.hora_entrada, Asistencia.hora_salida " +
                           "FROM Asistencia " +
                           "JOIN Empleado ON Asistencia.id_empleado = Empleado.id_empleado " +
                           "WHERE Asistencia.fecha = @fecha AND Empleado.id_empleado = @id";

            using (SqlConnection connection = conexion.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Agregar los parámetros a la consulta
                        command.Parameters.AddWithValue("@fecha", fecha);
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string fechaAsistencia = reader.GetDateTime(0).ToString("yyyy-MM-dd");
                                string idEmpleado = reader.GetInt32(1).ToString();
                                string nombreEmpleado = reader.GetString(2);
                                string apellidoEmpleado = reader.GetString(3);
                                string horaEntrada = reader.GetTimeSpan(4).ToString(@"hh\:mm");
                                string horaSalida = reader.GetTimeSpan(5).ToString(@"hh\:mm");

                                // Agregar los resultados a la lista correcta
                               

                                listaAsistenciaMostrar.Add($"{fechaAsistencia}|{idEmpleado}|{nombreEmpleado}|{apellidoEmpleado}|{horaEntrada}|{horaSalida}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener la información: " + ex.Message);
                }
            }

            return listaAsistenciaMostrar;
        }



        public bool ActualizarAsistencia(int idEmpleado, string fechaAsistencia, string horaEntrada, string horaSalida)
        {
            // Definir la consulta SQL para actualizar la asistencia
            string query = "UPDATE Asistencia " +
                           "SET hora_entrada = @horaEntrada, hora_salida = @horaSalida " +
                           "WHERE id_empleado = @idEmpleado AND fecha = @fechaAsistencia";

            using (SqlConnection connection = conexion.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Agregar los parámetros a la consulta SQL
                        command.Parameters.AddWithValue("@horaEntrada", horaEntrada);
                        command.Parameters.AddWithValue("@horaSalida", horaSalida);
                        command.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                        command.Parameters.AddWithValue("@fechaAsistencia", fechaAsistencia);

                        // Ejecutar la consulta y verificar cuántas filas fueron afectadas
                        int filasAfectadas = command.ExecuteNonQuery();

                        // Si se afectó al menos una fila, la actualización fue exitosa
                        return filasAfectadas > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al actualizar la asistencia: " + ex.Message);
                    return false; // En caso de error, devolver false
                }
            }
        }




        public Boolean RegistrarAsistenciaEntrada(int idEmpleado, DateTime fecha, TimeSpan horaEntrada, TimeSpan horaSalida, string ausencia)
        {
            Boolean result = false;
           

            string query = "INSERT INTO Asistencia (id_empleado, fecha, hora_entrada, hora_salida, ausencia) " +
                           "VALUES (@idEmpleado, @fecha, @horaEntrada, @horaSalida, @ausencia)";

            Console.WriteLine("Comando SQL: " + query); // Para depuración

            try
            {
                using (SqlConnection connection = conexion.GetConnection())
                {
                    connection.Open();

                    using (SqlCommand commandInsert = new SqlCommand(query, connection))
                    {
                        commandInsert.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                        commandInsert.Parameters.AddWithValue("@fecha", fecha);

                        commandInsert.Parameters.AddWithValue("@horaEntrada", horaEntrada);

                        // Si horaSalida es null, usa DBNull.Value
                        commandInsert.Parameters.AddWithValue("@horaSalida", horaSalida);

                        // Si ausencia es null, usa DBNull.Value
                        commandInsert.Parameters.AddWithValue("@ausencia", string.IsNullOrEmpty(ausencia) ? (object)DBNull.Value : ausencia);

                        commandInsert.ExecuteNonQuery();
                        result = true;
                    }
                }

                Console.WriteLine("Asistencia registrada correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar la asistencia: {ex.Message}");
            }

            return result;
        }


        public Boolean RegistrarHoraSalida(int idEmpleado, DateTime fecha, TimeSpan nuevaHoraSalida)
        {
            Boolean result = false;

            string query = "UPDATE Asistencia SET hora_salida = @nuevaHoraSalida " +
                           "WHERE id_empleado = @idEmpleado AND fecha = @fecha";

            Console.WriteLine("Comando SQL: " + query); // Para depuración

            try
            {
                using (SqlConnection connection = conexion.GetConnection())
                {
                    connection.Open();

                    using (SqlCommand commandUpdate = new SqlCommand(query, connection))
                    {
                        commandUpdate.Parameters.AddWithValue("@nuevaHoraSalida", nuevaHoraSalida);
                        commandUpdate.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                        commandUpdate.Parameters.AddWithValue("@fecha", fecha);

                        int filasAfectadas = commandUpdate.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            result = true;
                            Console.WriteLine("Hora de salida actualizada correctamente.");
                        }
                        else
                        {
                            Console.WriteLine("No se encontró el registro para actualizar.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la hora de salida: {ex.Message}");
            }

            return result;
        }




        public List<string> EmpleadosSinAsistencia(DateTime fecha)
        {
            List<string> listado = new List<string>();

            using (SqlConnection connection = conexion.GetConnection())
            {
                connection.Open();

                // Consulta corregida: Obtiene empleados que NO tienen asistencia en la fecha
                string query = @"
SELECT Empleado.id_empleado, Empleado.nombre, Empleado.apellidos 
FROM Empleado
LEFT JOIN Asistencia ON Asistencia.id_empleado = Empleado.id_empleado 
    AND Asistencia.fecha = @fecha
WHERE Asistencia.id_empleado IS NULL OR Asistencia.ausencia IS NOT NULL;";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fecha", fecha);

                    using (var dataAdapter = new SqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            // Concatenar id_empleado, nombre y apellidos
                            string empleado = $"{row["id_empleado"]}|{row["nombre"]} {row["apellidos"]}";
                            listado.Add(empleado);
                        }
                    }
                }
            }

            return listado;
        }


        public List<string> EmpleadosSinSalida(DateTime fecha)
        {
            List<string> listado = new List<string>();

            using (SqlConnection connection = conexion.GetConnection())
            {
                connection.Open();

                // Consulta corregida: Obtiene empleados que NO tienen asistencia en la fecha
                string query = @"SELECT Empleado.id_empleado, Empleado.nombre, Empleado.apellidos 
                                FROM Empleado
                                INNER JOIN Asistencia ON Asistencia.id_empleado = Empleado.id_empleado 
                                WHERE Asistencia.fecha = @fecha 
                                AND Asistencia.hora_entrada <> '00:00'
                                AND Asistencia.hora_salida = '00:00'
                                AND Asistencia.ausencia IS NULL;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fecha", fecha);

                    using (var dataAdapter = new SqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            // Concatenar id_empleado, nombre y apellidos
                            string empleado = $"{row["id_empleado"]}|{row["nombre"]} {row["apellidos"]}";
                            listado.Add(empleado);
                        }
                    }
                }
            }

            return listado;
        }



        public string ObtenerAusenciaPorEmpleadoYFecha(int id, string fecha)
        {
            string ausencia = null; // Valor predeterminado (null) si no hay ausencia registrada
            string query = "SELECT Asistencia.ausencia " +
                           "FROM Asistencia " +
                           "WHERE Asistencia.fecha = @fecha AND Asistencia.id_empleado = @id";

            using (SqlConnection connection = conexion.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Agregar los parámetros a la consulta
                        command.Parameters.AddWithValue("@fecha", fecha);
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Si se encuentra un registro
                            {
                                // Si el valor de 'ausencia' es null, se asignará null a la variable
                                ausencia = reader.IsDBNull(0) ? null : reader.GetString(0);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener la información: " + ex.Message);
                }
            }

            return ausencia;
        }

        public bool ActualizarAusencia(int id, string fecha, string ausencia)
        {    
            bool exito = false; // Valor por defecto (false) si no se actualizó correctamente
            string query = "UPDATE Asistencia SET ausencia = @ausencia " +
                           "WHERE id_empleado = @id AND fecha = @fecha";

            using (SqlConnection connection = conexion.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Agregar los parámetros a la consulta
                        command.Parameters.AddWithValue("@ausencia", ausencia);
                        command.Parameters.AddWithValue("@fecha", fecha);
                        command.Parameters.AddWithValue("@id", id);

                        int rowsAffected = command.ExecuteNonQuery();
                        // Si se actualizó al menos una fila, la operación fue exitosa
                        if (rowsAffected > 0)
                        {
                            exito = true;
                        }
                    }
                }
                catch (Exception)
                {
                    exito = false; // Si ocurre algún error, la operación no fue exitosa
                }
            }

            return exito;
        }




    }





}

