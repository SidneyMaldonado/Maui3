using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3
{
    public class Cidade
    {
        public int CdCidade { get; set; }
        public string? NmCidade { get; set; }
        public int CdUf { get; set; }
        public int CdUsuarioInc { get; set; }
        public int CdUsuarioAlt { get; set; }
        public DateTime DtInclusao { get; set; }
        public DateTime DtAlteracao { get; set; }
        public bool DmSituacao { get; set; }
    }
}
