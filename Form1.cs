namespace appCompilador;

using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public partial class Form1 : Form
{
    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    private const int MOD_CONTROL = 0x0002;
    private const int WM_HOTKEY = 0x0312;
    private const int HOTKEY_ID_CTRL_N = 1;
    private const int HOTKEY_ID_CTRL_S = 2;
    private const int HOTKEY_ID_CTRL_O = 3;
    private const int HOTKEY_ID_F7 = 4;
    private const int HOTKEY_ID_F1 = 5;

    private bool newFile = true;
    private string path = "";

    public Form1()
    {
        InitializeComponent();
        FormClosing += (s, e) =>
        {
            UnregisterHotKey(Handle, HOTKEY_ID_CTRL_N);
            UnregisterHotKey(Handle, HOTKEY_ID_CTRL_S);
            UnregisterHotKey(Handle, HOTKEY_ID_CTRL_O);
            UnregisterHotKey(Handle, HOTKEY_ID_F7);
            UnregisterHotKey(Handle, HOTKEY_ID_F1);
        };

        // Registrando CTRL + N
        RegisterHotKey(Handle, HOTKEY_ID_CTRL_N, MOD_CONTROL, (int)Keys.N);
        // Registrando CTRL + S
        RegisterHotKey(Handle, HOTKEY_ID_CTRL_S, MOD_CONTROL, (int)Keys.S);
        // Registrando CTRL + O
        RegisterHotKey(Handle, HOTKEY_ID_CTRL_O, MOD_CONTROL, (int)Keys.O);
        // Registrando F7
        RegisterHotKey(Handle, HOTKEY_ID_F7, 0, (int)Keys.F7);
        // Registrando F1
        RegisterHotKey(Handle, HOTKEY_ID_F1, 0, (int)Keys.F1);
    }
    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WM_HOTKEY)
        {
            int id = m.WParam.ToInt32();

            if (id == HOTKEY_ID_CTRL_N)
            {
                // Handle CTRL + N hotkey
                buttonNew_Click(this, EventArgs.Empty);
            }
            else if (id == HOTKEY_ID_CTRL_O)
            {
                // Handle CTRL + O hotkey
                buttonOpen_Click(this, EventArgs.Empty);
            }
            if (id == HOTKEY_ID_CTRL_S)
            {
                // Handle CTRL + S hotkey
                buttonSave_Click(this, EventArgs.Empty);
            }
            else if (id == HOTKEY_ID_F7)
            {
                // Handle F7 hotkey
                buttonCompile_Click(this, EventArgs.Empty);
            }
            else if (id == HOTKEY_ID_F1)
            {
                // Handle F7 hotkey
                buttonTeam_Click(this, EventArgs.Empty);
            }
        }

        base.WndProc(ref m);
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void ClearAll()
    {
        editor.Clear();
        messageArea.Clear();
        statusBarLabel.Text = "";
    }

    private void editor_TextChanged(object sender, EventArgs e)
    {
        //panelLineNumbers.Refresh();
    }

    private void editor_VScroll(object sender, EventArgs e)
    {
        //panelLineNumbers.Refresh();
    }

    private void panel_Paint(object sender, PaintEventArgs e)
    {
        //TODO: arrumar numera��o das linhas

        Font font = editor.Font;
        int linhaAtual = editor.GetLineFromCharIndex(editor.SelectionStart);
        int linhasVisiveis = editor.ClientSize.Height / font.Height;

        for (int i = 0; i < linhasVisiveis; i++)
        {
            string numeroLinha = (linhaAtual + i + 1).ToString();
            e.Graphics.DrawString(numeroLinha, font, Brushes.Black, 2, i * font.Height);
        }
    }

    //TODO: implementar chamada para as teclas de atalhos
    private void button_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Control)
            buttonNew_Click(sender, e);

        else if (e.Control)
            buttonOpen_Click(sender, e);

        else if (e.Control)
            buttonSave_Click(sender, e);

        else if (e.Control && e.KeyCode == Keys.C)
            throw new NotImplementedException();    //TODO: criar m�todo para copiar

        else if (e.Control && e.KeyCode == Keys.V)
            throw new NotImplementedException();    //TODO: criar m�todo para colar

        else if (e.Control && e.KeyCode == Keys.X)
            throw new NotImplementedException();    //TODO: criar m�todo para cortar

        else if (e.KeyCode == Keys.F7)
            throw new NotImplementedException();    //TODO: criar m�todo para compilar

        else if (e.KeyCode == Keys.F1)
            throw new NotImplementedException();    //TODO: criar m�todo para mostrar equipe
    }

    private void buttonNew_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    private void buttonOpen_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Arquivos de Texto (*.txt)|*.txt|Todos os arquivos (*.*)|*.*";

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                editor.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
                messageArea.Clear();
                statusBarLabel.Text = openFileDialog.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir o arquivo: " + ex.Message);
            }
        }
    }

    private void buttonSave_Click(object sender, EventArgs e)
    {
        if (newFile && path == "")
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Arquivos de Texto (*.txt)|*.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    editor.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                    path = saveFileDialog.FileName;
                    newFile = false;

                    messageArea.Clear();
                    statusBarLabel.Text = path;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar o arquivo: " + ex.Message);
                }
            }
        }

        else
        {
            try
            {
                editor.SaveFile(path, RichTextBoxStreamType.PlainText);
                messageArea.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar o arquivo: " + ex.Message);
            }
        }
    }

    private void buttonCopy_Click(object sender, EventArgs e)
    {

    }

    private void buttonPaste_Click(object sender, EventArgs e)
    {

    }

    private void buttonCut_Click(object sender, EventArgs e)
    {

    }

    private void buttonCompile_Click(object sender, EventArgs e)
    {
        messageArea.Clear();
        messageArea.Text = "compila��o de programas ainda n�o foi implementada";
    }

    private void buttonTeam_Click(object sender, EventArgs e)
    {
        messageArea.Clear();
        messageArea.Text = "Pedro, Marlon e Sara";
    }


}