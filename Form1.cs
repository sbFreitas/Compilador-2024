namespace appCompilador;

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

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
    private const int HOTKEY_ID_CTRL_C = 6;
    private const int HOTKEY_ID_CTRL_V = 7;
    private const int HOTKEY_ID_CTRL_X = 8;

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
            UnregisterHotKey(Handle, HOTKEY_ID_CTRL_C);
            UnregisterHotKey(Handle, HOTKEY_ID_CTRL_V);
            UnregisterHotKey(Handle, HOTKEY_ID_CTRL_X);

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
        // Registrando CTRL + C
        RegisterHotKey(Handle, HOTKEY_ID_CTRL_C, MOD_CONTROL, (int)Keys.C);
        // Registrando CTRL + V
        RegisterHotKey(Handle, HOTKEY_ID_CTRL_V, MOD_CONTROL, (int)Keys.V);
        // Registrando CTRL + X
        RegisterHotKey(Handle, HOTKEY_ID_CTRL_X, MOD_CONTROL, (int)Keys.X);


        editor.TextChanged += Editor_TextChanged;
        editor.VScroll += Editor_VScroll;

        // Handle the panel's Paint event
        panelLineNumbers.Paint += Panel_Paint;

        // Handle the Layout event to update line numbers after the RichTextBox is fully laid out
        editor.Layout += Editor_VScroll;

        // Trigger line number drawing on form load
        this.Load += Form1_Load;
    }
    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WM_HOTKEY)
        {
            int id = m.WParam.ToInt32();

            if (id == HOTKEY_ID_CTRL_N)
            {
                // Handle CTRL + N hotkey
                ButtonNew_Click(this, EventArgs.Empty);
            }
            else if (id == HOTKEY_ID_CTRL_O)
            {
                // Handle CTRL + O hotkey
                ButtonOpen_Click(this, EventArgs.Empty);
            }
            if (id == HOTKEY_ID_CTRL_S)
            {
                // Handle CTRL + S hotkey
                ButtonSave_Click(this, EventArgs.Empty);
            }
            else if (id == HOTKEY_ID_F7)
            {
                // Handle F7 hotkey
                ButtonCompile_Click(this, EventArgs.Empty);
            }
            else if (id == HOTKEY_ID_F1)
            {
                // Handle F1 hotkey
                ButtonTeam_Click(this, EventArgs.Empty);
            }
            else if (id == HOTKEY_ID_CTRL_C)
            {
                // Handle CTRL + C hotkey
                ButtonCopy_Click(this, EventArgs.Empty);
            }
            else if (id == HOTKEY_ID_CTRL_V)
            {
                // Handle CTRL + V hotkey
                ButtonPaste_Click(this, EventArgs.Empty);
            }
            else if (id == HOTKEY_ID_CTRL_X)
            {
                // Handle CTRL + X hotkey
                ButtonCut_Click(this, EventArgs.Empty);
            }
        }

        base.WndProc(ref m);
    }

    private void Form1_Load(object? sender, EventArgs e)
    {

    }

    private void ClearAll()
    {
        editor.Clear();
        messageArea.Clear();
        statusBarLabel.Text = "";
    }

    private void Editor_TextChanged(object? sender, EventArgs e)
    {
        UpdateLineNumbers();
    }

    private void Editor_VScroll(object? sender, EventArgs e)
    {
        panelLineNumbers.Invalidate();
    }

    private void UpdateLineNumbers()
    {
        // Get the first visible line index
        int firstIndex = editor.GetCharIndexFromPosition(new Point(0, 0));
        int firstLine = editor.GetLineFromCharIndex(firstIndex);

        // Get the total number of lines in the RichTextBox
        int totalLines = editor.Lines.Length;

        // Draw the line numbers
        using (Graphics g = panelLineNumbers.CreateGraphics())
        {
            g.Clear(panelLineNumbers.BackColor);

            g.DrawString((firstLine + 1).ToString(), editor.Font, Brushes.Black, new PointF(0, 0));

            for (int i = firstLine; i < totalLines; i++)
            {
                int y = editor.GetPositionFromCharIndex(editor.GetFirstCharIndexFromLine(i)).Y;

                g.DrawString((i + 1).ToString(), editor.Font, Brushes.Black, new PointF(0, y));
            }
        }
    }

    private void Panel_Paint(object? sender, PaintEventArgs e)
    {
        UpdateLineNumbers();
    }


    private void ButtonNew_Click(object? sender, EventArgs e)
    {
        ClearAll();
    }

    private void ButtonOpen_Click(object? sender, EventArgs e)
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

    private void ButtonSave_Click(object? sender, EventArgs e)
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

    private void ButtonCopy_Click(object? sender, EventArgs e)
    {
        if (editor.SelectedText != "")
        {
            editor.Copy(); // Copia o texto selecionado para a área de transferência
        }
    }

    private void ButtonPaste_Click(object? sender, EventArgs e)
    {
        if (Clipboard.ContainsText())
        {
            editor.Paste(); // Cola o texto da área de transferência no editor
        }
    }

    private void ButtonCut_Click(object? sender, EventArgs e)
    {
        if (editor.SelectedText != "")
        {
            editor.Cut(); // Corta o texto selecionado e o coloca na área de transferência
        }
    }

    private void ButtonCompile_Click(object? sender, EventArgs e)
    {
        messageArea.Clear();
        //messageArea.Text = "compilação de programas ainda não foi implementada";

        Lexico lexico = new Lexico();
        string editorText = editor.Text;
        lexico.setInput(new StringReader(editorText));



        try
        {
            Token? t = null;

            messageArea.Text += "linha        classe             lexema" + Environment.NewLine;

            while ((t = lexico.nextToken()) != null)
            {


                int line = GetLineFromPosition(t.GetPosition());
                messageArea.Text += $"{line}          ";

                string tokenClass = GetTokenClassById(t.GetId());
                messageArea.Text += $"{tokenClass}                 ";

                messageArea.Text += t.GetLexeme() + Environment.NewLine;

            }

            messageArea.Text += "Programa compilado com sucesso\n";
        }

        catch (LexicalError error)
        {  // tratamento de erros
            if (error.Message.Equals("símbolo inválido"))
            {
                messageArea.Text = "linha " + GetLineFromPosition(error.GetPosition()) + $": {editor.Text[error.GetPosition()]} " + error.Message;
            } else
            if( error.Message.Equals("palavra reservada inválida") ||
                error.Message.Equals("identificador inválido"))
            {
                int position = error.GetPosition();
                string caracter = editor.Text[position].ToString();
                string text = caracter;
                try
                {
                    caracter = editor.Text[position + 1].ToString();
         
                    while (caracter != " " && caracter != "\n")
                    {
                        position++;
                        caracter = editor.Text[position].ToString();
                        text += caracter;
                    }
                }
                catch { }
                messageArea.Text = "linha " + GetLineFromPosition(error.GetPosition()) + $": {text} " + error.Message;
            } else
            {
                messageArea.Text = "linha " + GetLineFromPosition(error.GetPosition()) + $": "+ error.Message;

            }

        }
    }

    private void ButtonTeam_Click(object? sender, EventArgs e)
    {
        messageArea.Clear();
        messageArea.Text = "Pedro, Marlon e Sarah";
    }

    private void Editor_Layout(object? sender, LayoutEventArgs e)
    {
        UpdateLineNumbers();
    }

    private static string GetTokenClassById(int id)
    {
        switch(id) 
        {
            case 2: return "Identificador";
            case 3: return "Constante_int";
            case 4: return "Constante_float";
            case 5: return "Constante_string";
            case 6: return "Palavra";
            case int i when (i >= 7 && i <= 19): return "Palavra reservada";
            case int i when (i >= 20 && i <= 35): return "Símbolo Especial";
            default: return "Desconhecido";
        }
    }

    private int GetLineFromPosition(int position)
    {
        Point pos = editor.GetPositionFromCharIndex(position);
        int line = editor.GetLineFromCharIndex(editor.GetCharIndexFromPosition(pos));

        return line + 1;
    }
}

