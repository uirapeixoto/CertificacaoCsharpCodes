<Query Kind="Program">
  <Reference Relative="..\..\..\.nuget\packages\flunt.br\2.0.0\lib\netstandard2.0\Flunt.Br.dll">&lt;NuGet&gt;\flunt.br\2.0.0\lib\netstandard2.0\Flunt.Br.dll</Reference>
  <Reference Relative="..\..\..\.nuget\packages\flunt\2.0.5\lib\netstandard2.0\Flunt.dll">&lt;NuGet&gt;\flunt\2.0.5\lib\netstandard2.0\Flunt.dll</Reference>
  <Namespace>Flunt</Namespace>
  <Namespace>Flunt.Notifications</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations</Namespace>
  <Namespace>System.Diagnostics.CodeAnalysis</Namespace>
</Query>

void Main()
{
	var myClass = new MyClass
	{
		Nome = "John Doe",
		Email = "johndoe@email.com",
		Nascimento = DateTime.Now.AddDays(10)
	};
	
	List<Notification> notificacoes = ValidarObjeto(myClass);
	notificacoes.Dump("Validação");
}

// You can define other methods, fields, classes and namespaces here
public List<Notification> ValidarObjeto(MyClass request)
{
    var notificacoes = new List<Notification>();
    var contexto = new ValidationContext(request);
    var resultados = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
    var ehValido = Validator.TryValidateObject(request, contexto, resultados, true);
    if (!ehValido)
    {
        notificacoes = resultados.Select(resultado => new Notification(resultado.MemberNames.FirstOrDefault(), resultado.ErrorMessage)).ToList();
    }
	resultados.Dump("Criterios de validação");

    return notificacoes;
}


public class MyClass
{
	public int Id {get; set;}
	[Required(ErrorMessage = "O campo {0} é obrigatório.")]
	public string Nome {get; set;}
	[DateLessThanOrEqualToToday]
	public DateTime Nascimento {get; set;}
	[Required(ErrorMessage = "O campo {0} é obrigatório.")]
	public string Email {get; set;}
	[Required(ErrorMessage = "O campo {0} é obrigatório.")]
	public Guid UID {get; set;}
	[Range(1, int.MaxValue, ErrorMessage = "O campo {0} é obrigatório.")]
	public int Cotas {get; set;}
	[Required(ErrorMessage = "O campo {0} é obrigatório.")]
	[EnumValidation<Status>(ErrorMessage = "{0} inválida")]
	public Status Situacao {get; set;}
}

public enum Status
{
	Ativo,
	Inativo, 
	Bloqueado,
	AguardandoAprovacao
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class EnumValidationAttribute<TEnum> : ValidationAttribute where TEnum : Enum
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var inputString = value as string;

        if (inputString == null)
            return ValidationResult.Success;

        if (string.IsNullOrWhiteSpace(inputString))
        {
            return new ValidationResult("O valor de entrada não é uma string.");
        }
        char.TryParse(inputString, out char outchar);
        var enumValues = Enum.ToObject(typeof(TEnum), outchar);

        if (enumValues.ToString() == "0")
        {
            return new ValidationResult($"O valor não é válido para o tipo {typeof(TEnum).Name}.");
        }

        return ValidationResult.Success;
    }
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class DateLessThanOrEqualToToday : ValidationAttribute
{
    public override string FormatErrorMessage(string name)
    {
        return "Data deve ser menor que a data de hoje";
    }

    protected override ValidationResult IsValid(object objValue,
                                                   ValidationContext validationContext)
    {
        var dateValue = objValue as DateTime? ?? new DateTime();

        //alter this as needed. I am doing the date comparison if the value is not null

        if (dateValue.Date > DateTime.Now.Date)
        {
           return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
        return ValidationResult.Success;
    }
}