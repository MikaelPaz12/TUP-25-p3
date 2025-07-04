﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using servidor.Data;

#nullable disable

namespace servidor.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250614200356_AjusteCarritoController")]
    partial class AjusteCarritoController
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.6");

            modelBuilder.Entity("servidor.Modelos.Carrito", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Carritos");
                });

            modelBuilder.Entity("servidor.Modelos.Compra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ApellidoCliente")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailCliente")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<string>("NombreCliente")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Total")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("servidor.Modelos.ItemCompra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cantidad")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompraId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CompraId");

                    b.HasIndex("ProductoId");

                    b.ToTable("ItemsCompra");
                });

            modelBuilder.Entity("servidor.Modelos.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CarritoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImagenUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Precio")
                        .HasColumnType("TEXT");

                    b.Property<int>("Stock")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CarritoId");

                    b.ToTable("Productos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descripcion = "100 ml de vidrio",
                            ImagenUrl = "img/vaso1.jpg",
                            Nombre = "Vaso medidor",
                            Precio = 550m,
                            Stock = 10
                        },
                        new
                        {
                            Id = 2,
                            Descripcion = "500 ml, 220V",
                            ImagenUrl = "img/manta.jpg",
                            Nombre = "Manta calefactora",
                            Precio = 6800m,
                            Stock = 6
                        },
                        new
                        {
                            Id = 3,
                            Descripcion = "25 ml, vidrio borosilicato",
                            ImagenUrl = "img/pipeta.jpg",
                            Nombre = "Pipeta graduada",
                            Precio = 400m,
                            Stock = 15
                        },
                        new
                        {
                            Id = 4,
                            Descripcion = "250 ml",
                            ImagenUrl = "img/matraz.jpg",
                            Nombre = "Matraz Erlenmeyer",
                            Precio = 900m,
                            Stock = 9
                        },
                        new
                        {
                            Id = 5,
                            Descripcion = "Precisión 0.01g",
                            ImagenUrl = "img/balanza.jpg",
                            Nombre = "Balanza digital",
                            Precio = 5400m,
                            Stock = 3
                        },
                        new
                        {
                            Id = 6,
                            Descripcion = "Velocidad regulable",
                            ImagenUrl = "img/agitador.jpg",
                            Nombre = "Agitador magnético",
                            Precio = 8200m,
                            Stock = 4
                        },
                        new
                        {
                            Id = 7,
                            Descripcion = "x10 unidades",
                            ImagenUrl = "img/tubos.jpg",
                            Nombre = "Tubos de ensayo",
                            Precio = 750m,
                            Stock = 12
                        },
                        new
                        {
                            Id = 8,
                            Descripcion = "100 ml con base plástica",
                            ImagenUrl = "img/probeta.jpg",
                            Nombre = "Probeta",
                            Precio = 600m,
                            Stock = 11
                        },
                        new
                        {
                            Id = 9,
                            Descripcion = "Digital, resistente al agua",
                            ImagenUrl = "img/cronometro.jpg",
                            Nombre = "Cronómetro",
                            Precio = 950m,
                            Stock = 8
                        },
                        new
                        {
                            Id = 10,
                            Descripcion = "-50°C a 300°C",
                            ImagenUrl = "img/termometro.jpg",
                            Nombre = "Termómetro digital",
                            Precio = 1200m,
                            Stock = 7
                        });
                });

            modelBuilder.Entity("servidor.Modelos.ItemCompra", b =>
                {
                    b.HasOne("servidor.Modelos.Compra", "Compra")
                        .WithMany("Items")
                        .HasForeignKey("CompraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("servidor.Modelos.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compra");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("servidor.Modelos.Producto", b =>
                {
                    b.HasOne("servidor.Modelos.Carrito", null)
                        .WithMany("Productos")
                        .HasForeignKey("CarritoId");
                });

            modelBuilder.Entity("servidor.Modelos.Carrito", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("servidor.Modelos.Compra", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
