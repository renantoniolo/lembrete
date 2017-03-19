using System;
namespace lembrete
{
	public interface IReminderService
	{
		void Remind(DateTime dateTime, string title, string message);
	}
}
