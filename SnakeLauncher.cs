using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

internal static class SnakeLauncher
{
    [STAThread]
    private static void Main()
    {
        try
        {
            string gameDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "RetroSnake");
            Directory.CreateDirectory(gameDirectory);

            string gamePath = Path.Combine(gameDirectory, "snake.html");
            using (Stream game = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("SnakeGame.html"))
            {
                if (game == null)
                {
                    throw new InvalidOperationException("Die eingebettete Spieldatei fehlt.");
                }

                using (FileStream output = File.Create(gamePath))
                {
                    game.CopyTo(output);
                }
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = gamePath,
                UseShellExecute = true
            });
        }
        catch (Exception error)
        {
            MessageBox.Show(
                "Snake konnte nicht gestartet werden.\r\n\r\n" + error.Message,
                "Snake – Retro Edition",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
