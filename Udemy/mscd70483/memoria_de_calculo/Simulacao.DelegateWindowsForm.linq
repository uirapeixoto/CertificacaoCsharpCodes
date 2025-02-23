<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

void Main()
{
	  DisplayMessage messageTarget;
	  
      if (Environment.GetCommandLineArgs().Length > 1)
         messageTarget = ShowWindowsMessage;
      else
         messageTarget = Console.WriteLine;

      messageTarget("Hello, World!");
}

// Define other methods and classes here
/*
	Um delegado é um tipo que representa referências aos métodos com lista de parâmetros e tipo de retorno 
	específicos. Ao instanciar um delegado, você pode associar sua instância a qualquer método com assinatura 
	e tipo de retorno compatíveis. 
	Você pode invocar (ou chamar) o método através da instância de delegado.
*/
	delegate void DisplayMessage(string message);

	private static void ShowWindowsMessage(string message)
   {
      MessageBox.Show(message);
   }