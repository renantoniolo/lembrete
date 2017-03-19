using System;
using System.Threading.Tasks;

namespace lembrete
{
	public interface IAlertService
	{
		Task ShowAsync(string title, string message, string buttonOk);
		Task ShowAsync(string title, string message, string buttonOk, string buttonCancel);
	}
}
