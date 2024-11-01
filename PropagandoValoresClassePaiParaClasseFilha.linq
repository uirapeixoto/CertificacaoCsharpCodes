<Query Kind="Program">
  <Reference Relative="Libs\autobogus\2.13.1\lib\netstandard2.0\AutoBogus.dll">&lt;UserProfile&gt;\OneDrive - Localiza\Documentos\LINQPad Queries\Libs\autobogus\2.13.1\lib\netstandard2.0\AutoBogus.dll</Reference>
  <Reference Relative="Libs\bogus\33.0.2\lib\netstandard2.0\Bogus.dll">&lt;UserProfile&gt;\OneDrive - Localiza\Documentos\LINQPad Queries\Libs\bogus\33.0.2\lib\netstandard2.0\Bogus.dll</Reference>
  <Namespace>AutoBogus</Namespace>
  <Namespace>Bogus</Namespace>
  <Namespace>Bogus.Extensions.Brazil</Namespace>
  <Namespace>System.Diagnostics.CodeAnalysis</Namespace>
</Query>

void Main()
{
	var classebase = new ClasseBase{
		Id = 1, 
		Valor = "Valor",
		DataCriacao = DateTime.Now,
		Usuario = "John Doe",
		Derivada = new ClasseDerivada
		{	
			Id = 1,
			ValorDerivado = "Valor derivada"
		}
	}.Dump("Resultado"); 
}

// You can define other methods, fields, classes and namespaces here
public class ClasseBase
{
	private ClasseDerivada _derivada;

	public ClasseBase()
	{
		Derivada = new ClasseDerivada();
		Derivada.Usuario = Usuario;
	}

	public int Id {get; set;}
	public string Valor {get; set;}
	public string Usuario {get; set;}
	public DateTime DataCriacao {get; set;}
	public ClasseDerivada Derivada {
		get => _derivada; 
		set {
			_derivada = value;
			if(_derivada is not null)
			{
				_derivada.Usuario = Usuario;
				_derivada.DataCriacao = DataCriacao;
			}
		}
	}
	
}

public class ClasseDerivada
{	
	public int Id { get; set;}
	public string ValorDerivado {get; set;}
	public string Usuario {get; set;}
	public DateTime DataCriacao {get; set;}
}