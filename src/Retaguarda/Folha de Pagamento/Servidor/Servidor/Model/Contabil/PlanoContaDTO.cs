using System;
using System.Text;
using System.Collections.Generic;


namespace Servidor.Model {
    
    public class PlanoContaDTO {
        public PlanoContaDTO() { }
        public int Id { get; set; }
        public string Nome { get; set; }
        public System.Nullable<System.DateTime> DataInclusao { get; set; }
        public string Mascara { get; set; }
        public System.Nullable<int> Niveis { get; set; }
    }
}
