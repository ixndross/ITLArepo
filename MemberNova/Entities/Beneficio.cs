﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Beneficios")]
public class Beneficio
{
    [Key]
    [Column("ID")]
    public int BiD { get; set; }

    [MaxLength(60)]
    public string Nombre { get; set; }

    [MaxLength(500)]
    public string Descripcion { get; set; }

    //Llave foranea dentro de la base de datos, relacionando el beneficio seleccionado con una membresia con ID unico.
    [ForeignKey("TipoMembresia")]
    public int TipoMembresia { get; set; }
    //Referencia de navegacion a la entidad Membresia.
    public Membresia Membresia { get; set; } = null;

    public Beneficio()
    {
        //Constructor por defecto
    }

    public Beneficio(string nombre, string descripcion, int tipoMembresia)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        TipoMembresia = 0;
    }  
}