using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Entidades.Models.Cadastros;

namespace Entidades.EF
{
    public class Contexto : DbContext
    {
        public DbSet<Caminhao> Caminhoes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Password=DUARTE501313!;Persist Security Info=True;User ID=sa;Initial Catalog=Volvo;Data Source=DESKTOP-6AD5HNH");

        }
    }
}
