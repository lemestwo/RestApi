using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApi.Models
{
    public enum Size : byte
    {
        Pequeno = 1,
        Medio = 2,
        Grande = 3
    }

    public enum Flavor : byte
    {
        Morango = 1,
        Banana = 2,
        Kiwi = 3
    }

    public class Product
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 3)]
        public Size Size { get; set; }

        [Required]
        [Range(1, 3)]
        public Flavor Flavor { get; set; }

        public bool Custom1 { get; set; }

        public bool Custom2 { get; set; }

        public bool Custom3 { get; set; }

        [NotMapped]
        public int Time { get => getSizeTime() + getBaseTime() + getCustomTime(); }

        [NotMapped]
        public int TimeNoCustom { get => getSizeTime() + getBaseTime(); }

        [NotMapped]
        public int TimeOnlyCustom { get => getCustomTime(); }

        [NotMapped]
        public int Value { get => getSizeValue() + getCustomValue(); }

        private int getSizeValue()
        {
            switch (Size)
            {
                case Size.Pequeno:
                    return 10;
                case Size.Medio:
                    return 13;
                case Size.Grande:
                    return 15;
            }
            return 0;
        }

        private int getCustomValue()
        {
            var final = 0;
            if (Custom1) final += 3;
            if (Custom3) final += 3;
            return final;
        }

        private int getSizeTime()
        {
            switch (Size)
            {
                case Size.Pequeno:
                    return 5;
                case Size.Medio:
                    return 7;
                case Size.Grande:
                    return 10;
            }
            return 0;
        }

        private int getBaseTime() => (Flavor == Flavor.Kiwi) ? 5 : 0;

        private int getCustomTime() => (Custom3) ? 3 : 0;
    }
}
