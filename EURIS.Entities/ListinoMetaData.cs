using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EURIS.Entities
{
    [MetadataType(typeof(ListinoMetaData))]
    public partial class Listino
    {
    }

    internal sealed class ListinoMetaData
    {
        [Required(ErrorMessage = "Il Codice listino è richiesto")]
        public string codice;
    }
}
