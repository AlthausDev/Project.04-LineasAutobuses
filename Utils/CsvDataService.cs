﻿using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace AvilesaBusManagementSystem.Utils
{
    /// <summary>
    /// Clase genérica para leer y escribir datos desde/hacia archivos CSV.
    /// </summary>
    /// <typeparam name="T">El tipo de datos que se leerán o escribirán desde/hacia el archivo CSV.</typeparam>
    public class CsvDataService<T>
    {
        private readonly string FileName;
        private readonly string FilePath;

        private static readonly string Path = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string DataFolder = "Data";

        /// <summary>
        /// Configuración para el lector y escritor CSV.
        /// </summary>
        public CsvConfiguration Configuration { get; }

        /// <summary>
        /// Constructor de la clase CsvDataService.
        /// </summary>
        /// <param name="fileName">Nombre del archivo CSV.</param>
        public CsvDataService(string fileName)
        {
            FileName = fileName;
            FilePath = System.IO.Path.Combine(Path, DataFolder, FileName);

            Debug.WriteLine($"Ruta del archivo CSV: {FilePath}");


            Configuration = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                Delimiter = ";",
                Encoding = Encoding.Latin1,
                HasHeaderRecord = true,
                HeaderValidated = null, 
                MissingFieldFound = null
            };
        }

        /// <summary>
        /// Lee los datos desde el archivo CSV y los carga en una colección observable.
        /// </summary>
        /// <returns>Una colección observable de tipo T.</returns>
        public ObservableCollection<T> ReadFromCsv()
        {
            try
            {
                using (var reader = new StreamReader(FilePath, Encoding.Latin1))
                using (var csv = new CsvReader(reader, Configuration))
                {
                    return new ObservableCollection<T>(csv.GetRecords<T>());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al leer desde el archivo CSV {FilePath}: {ex.Message}");
                return new ObservableCollection<T>();
            }
        }

        /// <summary>
        /// Escribe los registros en el archivo CSV.
        /// </summary>
        /// <param name="records">La lista de registros a escribir en el archivo CSV.</param>
        public void WriteToCsv(ObservableCollection<T> records)
        {
            try
            {
                using (var writer = new StreamWriter(FilePath, false, Encoding.Latin1))
                using (var csv = new CsvWriter(writer, Configuration))
                {
                    csv.WriteRecords(records);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al escribir en el archivo CSV {FilePath}: {ex.Message}");
            }
        }

        public long GetLastLineaNumber()
        {
            var lineas = ReadFromCsv().OfType<IHasNumeroLinea>();
            if (lineas.Any())
            {
                return lineas.Max(l => l.NumeroLinea);
            }
            else
            {
                return 0;
            }
        }


    }
}
