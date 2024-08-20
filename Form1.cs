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
        //TODO: arrumar numeração das linhas

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
            throw new NotImplementedException();    //TODO: criar método para copiar

        else if (e.Control && e.KeyCode == Keys.V)
            throw new NotImplementedException();    //TODO: criar método para colar

        else if (e.Control && e.KeyCode == Keys.X)
            throw new NotImplementedException();    //TODO: criar método para cortar

        else if (e.KeyCode == Keys.F7)
            throw new NotImplementedException();    //TODO: criar método para compilar

        else if (e.KeyCode == Keys.F1)
            throw new NotImplementedException();    //TODO: criar método para mostrar equipe
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

    }

    private void buttonTeam_Click(object sender, EventArgs e)
    {

    }
}