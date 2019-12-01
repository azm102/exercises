using System.Collections.Generic;

namespace App
{
    public class Warehouse
    {
        public Warehouse(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public List<Cargo> Cargos { get; } = new List<Cargo>();

        public override string ToString() => Name;
    }
}