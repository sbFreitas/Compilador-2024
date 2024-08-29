namespace appCompilador;

using System.Drawing;
using System.Windows.Forms;

public partial class Form1 : Form
{

    private bool newFile = true;
    private string path = "";

    public Form1()
    {
        InitializeComponent();
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

    private void richTextBox1_TextChanged(object sender, EventArgs e)
    {
        panel.Refresh();
    }

    private void RichTextBox1_VScroll(object sender, EventArgs e)
    {
        panel.Refresh();
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
        if (e.Control && e.KeyCode == Keys.N)
            buttonNew_Click(sender, e);

        else if (e.Control && e.KeyCode == Keys.O)
            buttonOpen_Click(sender, e);

        else if (e.Control && e.KeyCode == Keys.S)
            buttonSave_Click(sender, e);

        else if (e.Control && e.KeyCode == Keys.C)
            ButtonCopy_Click(sender, e);    //TODO: criar m�todo para copiar

        else if (e.Control && e.KeyCode == Keys.V)
            ButtonPaste_Click(sender, e);    //TODO: criar m�todo para colar

        else if (e.Control && e.KeyCode == Keys.X)
            ButtonCut_Click(sender, e);    //TODO: criar m�todo para cortar

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
                    editor.SaveFile(saveFileDialog.FileName);
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
                editor.SaveFile(path);
                messageArea.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar o arquivo: " + ex.Message);
            }
        }
    }

    private void ButtonCopy_Click(object sender, EventArgs e)
    {
        if (editor.SelectedText != "")
        {
            editor.Copy(); // Copia o texto selecionado para a �rea de transfer�ncia
        }
    }

    private void ButtonPaste_Click(object sender, EventArgs e)
    {
        if (Clipboard.ContainsText())
        {
            editor.Paste(); // Cola o texto da �rea de transfer�ncia no editor
        }
    }

    private void ButtonCut_Click(object sender, EventArgs e)
    {
        if (editor.SelectedText != "")
        {
            editor.Cut(); // Corta o texto selecionado e o coloca na �rea de transfer�ncia
        }
    }

    private void buttonCompile_Click(object sender, EventArgs e)
    {

    }

    private void buttonTeam_Click(object sender, EventArgs e)
    {

    }
}