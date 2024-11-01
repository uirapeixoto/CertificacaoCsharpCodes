<Query Kind="Program">
  <Reference Relative="Libs\autobogus\2.13.1\lib\netstandard2.0\AutoBogus.dll">&lt;UserProfile&gt;\OneDrive - Localiza\Documentos\LINQPad Queries\Libs\autobogus\2.13.1\lib\netstandard2.0\AutoBogus.dll</Reference>
  <Reference Relative="Libs\bogus\33.0.2\lib\netstandard2.0\Bogus.dll">&lt;UserProfile&gt;\OneDrive - Localiza\Documentos\LINQPad Queries\Libs\bogus\33.0.2\lib\netstandard2.0\Bogus.dll</Reference>
  <Reference Relative="..\..\..\.nuget\packages\microsoft.aspnetcore.jsonpatch\6.0.6\lib\netstandard2.0\Microsoft.AspNetCore.JsonPatch.dll">&lt;NuGet&gt;\microsoft.aspnetcore.jsonpatch\6.0.6\lib\netstandard2.0\Microsoft.AspNetCore.JsonPatch.dll</Reference>
  <Reference Relative="Libs\newtonsoft.json\12.0.2\lib\netstandard2.0\Newtonsoft.Json.dll">&lt;UserProfile&gt;\OneDrive - Localiza\Documentos\LINQPad Queries\Libs\newtonsoft.json\12.0.2\lib\netstandard2.0\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Namespace>AutoBogus</Namespace>
  <Namespace>Bogus</Namespace>
  <Namespace>Microsoft.AspNetCore.JsonPatch</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Runtime.Serialization.Formatters.Binary</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	// usuario a ser motificado	
	var jsonInvalido = "[{\"value\":1,\"path\":\"/Nome\",\"op\":\"replace\"},{\"value\":1,\"path\":\"/SobreNome\",\"op\":\"replace\"},{\"value\":\"2024-10-2911:55:00.000\",\"path\":\"/DataModificacao\",\"op\":\"replace\"}]";
	var jsonOk = "[{\"value\":1,\"path\":\"/Nome\",\"op\":\"replace\"},{\"value\":\"2024-10-2911:55:00.000\",\"path\":\"/DataModificacao\",\"op\":\"replace\"}]";
	var jsonRequest = JsonConvert.DeserializeObject<JsonPatchDocument<Usuario>>(jsonInvalido);//.Dump("Request");
	
	//jsonRequest.Dump("Request");
	
	if(ValidatePatchDocumento.ValidarPatchObject(jsonRequest))
		Console.WriteLine("ok");
}

// Define other methods and classes here

public class Usuario {
	public int Id {get; set;}
	public string Nome {get; set;}
	public string Email {get; set;}
	public DateTime DataNascimento { get; set;}
	[DoNotPatch]
	public DateTime DataCriacao {get; set;}
	[DoNotPatch]
	public DateTime DataModificacao {get; set;}
	[DoNotPatch]
	public List<Comentario> Comentarios {get; set;}
}

public class Comentario
{
	public int Id {get; set;}
	public int UsuarioId {get; set;}
	public string Texto {get; set;}
	public DateTime DataPublicacao {get; set;}
}

public static class GenericExetensions
{
	public static List<string> GetFields<T>(string[] exceptColumns = null, string[] setColumns = null)
	{
	    List<string> fields = [];
		
	    PropertyInfo[] props = typeof(T).GetProperties();
		
	    // Seleciona as columas indicadas
	    if (setColumns is not null && exceptColumns is null)
	        props = props.Where(x => setColumns.Contains(x.Name)).ToArray();
		
	    foreach (PropertyInfo prop in props)
	    {
			object[] attrs = prop.GetCustomAttributes(true);
			
			fields.Add(prop.Name);
	    }
	    return fields.ToList();
	}
	
	public static List<string> GetCustomFields<T>()
	{
	    List<string> fields = [];
	    PropertyInfo[] props = typeof(T).GetProperties();
		
	    foreach (PropertyInfo prop in props)
	    {
			object[] attrs = prop.GetCustomAttributes(true);
			
			foreach (object attr in attrs)
          	{
				if (attr is DoNotPatchAttribute doNotPatchFieldAttr)
              	{
					fields.Add(prop.Name);
			  	}
			}
	    }
	    return fields.ToList();
	}
}

public static class ValidatePatchDocumento
{
	public static bool ValidarPatchObject(JsonPatchDocument<Usuario> jsonPathObject)
	{
		//jsonPathObject.Dump("input");
	    var operations = jsonPathObject.Operations;
		var fields = GenericExetensions.GetFields<Usuario>().Dump("Atributos");
		var exceptions = GenericExetensions.GetCustomFields<Usuario>().Dump("Exceção");
		var path = operations.Where(x => !fields.Contains(x.path)).Select(p => p.path.TrimStart(['/'])).Dump("Path");
	    var diferenca = path.Where(p => !fields.Contains(p)).Dump("Diferença");
		
		if(operations.Any(x => !fields.Contains(x.path)))
		{
	        return true;
		}

	    return false;
	}
	
	private static bool ValidatePatchRequest(JsonPatchDocument<Usuario> patchDocument)
	{
	    var validationResult = false;
	    foreach (var item in patchDocument.Operations)
	    {
	        if (string.IsNullOrEmpty(item.op))
	        {
	            validationResult = false;
	            break;
	        }

	        if (item.op == "add" || item.op == "test" || item.op == "replace")
	        {
	            if (string.IsNullOrEmpty(item.path) || item.value == null)
	            {
	                validationResult = false;
	                break;
	            }
	        }

	        if (item.op == "copy" || item.op == "move")
	        {
	            if (string.IsNullOrEmpty(item.path) || item.from == null)
	            {
	                validationResult = false;
	                break;
	            }
	        }

	        if (item.op == "remove")
	        {
	            if (string.IsNullOrEmpty(item.path))
	            {
	                validationResult = false;
	                break;
	            }
	        }

	        validationResult = true;
	    }

	    return validationResult;
	}
	
	public static void Sanitize<T>(this Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<T> document) where T : class
	{
	    for (int i = document.Operations.Count - 1; i >= 0; i--)
	    {
	        string pathPropertyName = document.Operations[i].path.Split("/", StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

	        if (typeof(T).GetProperties().Where(p => p.IsDefined(typeof(DoNotPatchAttribute), true) && string.Equals(p.Name, pathPropertyName, StringComparison.CurrentCultureIgnoreCase)).Any())
	        {
	            // remove
	            document.Operations.RemoveAt(i); 

	            //todo: log removal
	        }
	    }
	}
}

[AttributeUsage(AttributeTargets.Property)]
public class DoNotPatchAttribute : Attribute;




