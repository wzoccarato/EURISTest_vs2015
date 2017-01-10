using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EURIS.Entities
{

    [MetadataType(typeof(ProdottoMetadata))]
    public partial class Prodotto
    {
        // No field here
    }

    internal sealed class ProdottoMetadata
    {
        [Required(ErrorMessage = "Il Codice prodotto è richiesto")]
        public string codice;
    
    }
}
