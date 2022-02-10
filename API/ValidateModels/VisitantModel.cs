using Domain.Entities;
using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace API.ValidateModels;
public class VisitantModel
{
    public VisitantModel(Name nameModel, string emailModel, string phoneModel, Document documentModel, bool activeModel)
    {
        NameModel = nameModel;
        EmailModel = emailModel;
        PhoneModel = phoneModel;
        DocumentModel = documentModel;
        ActiveModel = activeModel;
    }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Required attribute")]
    [StringLength(120, MinimumLength = 3)]
    public Name NameModel { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Required attribute")]
    [StringLength(300)]
    [EmailAddress]
    public string EmailModel { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Required attribute")]
    [StringLength(13, MinimumLength = 10)]
    [Phone]
    public string PhoneModel { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Required attribute")]
    [StringLength(14, MinimumLength = 11)]
    public Document DocumentModel { get; set; }

    [Required]
    public bool ActiveModel { get; set; }

    public Visitant GetVisitant()
    {
        return new Visitant(NameModel, EmailModel, PhoneModel, DocumentModel, ActiveModel);
    }
}