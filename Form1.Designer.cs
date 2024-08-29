namespace appCompilador;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        toolbar = new ToolStrip();
        buttonNew = new ToolStripButton();
        buttonOpen = new ToolStripButton();
        buttonSave = new ToolStripButton();
        buttonCopy = new ToolStripButton();
        buttonPaste = new ToolStripButton();
        buttonCut = new ToolStripButton();
        buttonCompile = new ToolStripButton();
        buttonTeam = new ToolStripButton();
        splitter = new Splitter();
        messageArea = new TextBox();
        statusBar = new StatusStrip();
        statusBarLabel = new ToolStripStatusLabel();
        editor = new RichTextBox();
        panelLineNumbers = new Panel();
        toolbar.SuspendLayout();
        statusBar.SuspendLayout();
        SuspendLayout();
        // 
        // toolbar
        // 
        toolbar.ImageScalingSize = new Size(20, 20);
        toolbar.Items.AddRange(new ToolStripItem[] { buttonNew, buttonOpen, buttonSave, buttonCopy, buttonPaste, buttonCut, buttonCompile, buttonTeam });
        toolbar.Location = new Point(0, 0);
        toolbar.MinimumSize = new Size(900, 70);
        toolbar.Name = "toolbar";
        toolbar.Size = new Size(900, 70);
        toolbar.TabIndex = 0;
        toolbar.Text = "toolStrip1";
        // 
        // buttonNew
        // 
        buttonNew.AutoSize = false;
        buttonNew.Image = (Image)resources.GetObject("buttonNew.Image");
        buttonNew.ImageScaling = ToolStripItemImageScaling.None;
        buttonNew.ImageTransparentColor = Color.Magenta;
        buttonNew.Name = "buttonNew";
        buttonNew.Size = new Size(108, 60);
        buttonNew.Text = "Novo [Ctrl-n]";
        buttonNew.TextAlign = ContentAlignment.BottomCenter;
        buttonNew.TextImageRelation = TextImageRelation.ImageAboveText;
        buttonNew.Click += ButtonNew_Click;
        // 
        // buttonOpen
        // 
        buttonOpen.AutoSize = false;
        buttonOpen.Image = (Image)resources.GetObject("buttonOpen.Image");
        buttonOpen.ImageScaling = ToolStripItemImageScaling.None;
        buttonOpen.ImageTransparentColor = Color.Magenta;
        buttonOpen.Name = "buttonOpen";
        buttonOpen.Size = new Size(108, 60);
        buttonOpen.Text = "Abrir [Ctrl-o]";
        buttonOpen.TextAlign = ContentAlignment.BottomCenter;
        buttonOpen.TextImageRelation = TextImageRelation.ImageAboveText;
        buttonOpen.Click += ButtonOpen_Click;
        // 
        // buttonSave
        // 
        buttonSave.AutoSize = false;
        buttonSave.Image = (Image)resources.GetObject("buttonSave.Image");
        buttonSave.ImageScaling = ToolStripItemImageScaling.None;
        buttonSave.ImageTransparentColor = Color.Magenta;
        buttonSave.Name = "buttonSave";
        buttonSave.Size = new Size(108, 60);
        buttonSave.Text = "Salvar [Ctrl-s]";
        buttonSave.TextAlign = ContentAlignment.BottomCenter;
        buttonSave.TextImageRelation = TextImageRelation.ImageAboveText;
        buttonSave.Click += ButtonSave_Click;
        // 
        // buttonCopy
        // 
        buttonCopy.AutoSize = false;
        buttonCopy.Image = (Image)resources.GetObject("buttonCopy.Image");
        buttonCopy.ImageScaling = ToolStripItemImageScaling.None;
        buttonCopy.ImageTransparentColor = Color.Magenta;
        buttonCopy.Name = "buttonCopy";
        buttonCopy.Size = new Size(108, 60);
        buttonCopy.Text = "Copiar [Ctrl-c]";
        buttonCopy.TextAlign = ContentAlignment.BottomCenter;
        buttonCopy.TextImageRelation = TextImageRelation.ImageAboveText;
        buttonCopy.Click += ButtonCopy_Click;
        // 
        // buttonPaste
        // 
        buttonPaste.AutoSize = false;
        buttonPaste.Image = (Image)resources.GetObject("buttonPaste.Image");
        buttonPaste.ImageScaling = ToolStripItemImageScaling.None;
        buttonPaste.ImageTransparentColor = Color.Magenta;
        buttonPaste.Name = "buttonPaste";
        buttonPaste.Size = new Size(108, 60);
        buttonPaste.Text = "Colar [Ctrl-v]";
        buttonPaste.TextAlign = ContentAlignment.BottomCenter;
        buttonPaste.TextImageRelation = TextImageRelation.ImageAboveText;
        buttonPaste.Click += ButtonPaste_Click;
        // 
        // buttonCut
        // 
        buttonCut.AutoSize = false;
        buttonCut.Image = (Image)resources.GetObject("buttonCut.Image");
        buttonCut.ImageScaling = ToolStripItemImageScaling.None;
        buttonCut.ImageTransparentColor = Color.Magenta;
        buttonCut.Name = "buttonCut";
        buttonCut.Size = new Size(109, 60);
        buttonCut.Text = "Recortar [Ctrl-x]";
        buttonCut.TextAlign = ContentAlignment.BottomCenter;
        buttonCut.TextImageRelation = TextImageRelation.ImageAboveText;
        buttonCut.Click += ButtonCut_Click;
        // 
        // buttonCompile
        // 
        buttonCompile.AutoSize = false;
        buttonCompile.Image = (Image)resources.GetObject("buttonCompile.Image");
        buttonCompile.ImageScaling = ToolStripItemImageScaling.None;
        buttonCompile.ImageTransparentColor = Color.Magenta;
        buttonCompile.Name = "buttonCompile";
        buttonCompile.Size = new Size(108, 60);
        buttonCompile.Text = "Compilar [F7]";
        buttonCompile.TextAlign = ContentAlignment.BottomCenter;
        buttonCompile.TextImageRelation = TextImageRelation.ImageAboveText;
        buttonCompile.Click += ButtonCompile_Click;
        // 
        // buttonTeam
        // 
        buttonTeam.AutoSize = false;
        buttonTeam.Image = (Image)resources.GetObject("buttonTeam.Image");
        buttonTeam.ImageScaling = ToolStripItemImageScaling.None;
        buttonTeam.ImageTransparentColor = Color.Magenta;
        buttonTeam.Name = "buttonTeam";
        buttonTeam.Size = new Size(108, 60);
        buttonTeam.Text = "Equipe [F1]";
        buttonTeam.TextAlign = ContentAlignment.BottomCenter;
        buttonTeam.TextImageRelation = TextImageRelation.ImageAboveText;
        buttonTeam.Click += ButtonTeam_Click;
        // 
        // splitter
        // 
        splitter.BackColor = Color.LightGray;
        splitter.Dock = DockStyle.Bottom;
        splitter.Location = new Point(0, 368);
        splitter.Name = "splitter";
        splitter.Size = new Size(900, 11);
        splitter.TabIndex = 2;
        splitter.TabStop = false;
        // 
        // messageArea
        // 
        messageArea.BackColor = Color.WhiteSmoke;
        messageArea.BorderStyle = BorderStyle.None;
        messageArea.Dock = DockStyle.Bottom;
        messageArea.Enabled = false;
        messageArea.Location = new Point(0, 379);
        messageArea.Multiline = true;
        messageArea.Name = "messageArea";
        messageArea.ReadOnly = true;
        messageArea.ScrollBars = ScrollBars.Both;
        messageArea.Size = new Size(900, 196);
        messageArea.TabIndex = 3;
        messageArea.WordWrap = false;
        // 
        // statusBar
        // 
        statusBar.AutoSize = false;
        statusBar.BackColor = SystemColors.Info;
        statusBar.ImageScalingSize = new Size(20, 20);
        statusBar.Items.AddRange(new ToolStripItem[] { statusBarLabel });
        statusBar.Location = new Point(0, 575);
        statusBar.Name = "statusBar";
        statusBar.Size = new Size(900, 25);
        statusBar.TabIndex = 4;
        statusBar.Text = "statusBar";
        // 
        // statusBarLabel
        // 
        statusBarLabel.Name = "statusBarLabel";
        statusBarLabel.Size = new Size(0, 19);
        // 
        // editor
        // 
        editor.AcceptsTab = true;
        editor.BorderStyle = BorderStyle.None;
        editor.Dock = DockStyle.Fill;
        editor.Location = new Point(27, 70);
        editor.Name = "editor";
        editor.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
        editor.Size = new Size(873, 298);
        editor.TabIndex = 0;
        editor.Text = "";
        editor.WordWrap = false;
        editor.VScroll += Editor_VScroll;
        editor.TextChanged += Editor_TextChanged;
        editor.Layout += Editor_Layout;
        // 
        // panelLineNumbers
        // 
        panelLineNumbers.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        panelLineNumbers.BackColor = SystemColors.Info;
        panelLineNumbers.Dock = DockStyle.Left;
        panelLineNumbers.Location = new Point(0, 70);
        panelLineNumbers.Name = "panelLineNumbers";
        panelLineNumbers.Size = new Size(27, 298);
        panelLineNumbers.TabIndex = 6;
        panelLineNumbers.Paint += Panel_Paint;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        ClientSize = new Size(900, 600);
        Controls.Add(editor);
        Controls.Add(panelLineNumbers);
        Controls.Add(splitter);
        Controls.Add(messageArea);
        Controls.Add(toolbar);
        Controls.Add(statusBar);
        MinimumSize = new Size(910, 600);
        Name = "Form1";
        Text = "Compilador";
        Load += Form1_Load;
        toolbar.ResumeLayout(false);
        toolbar.PerformLayout();
        statusBar.ResumeLayout(false);
        statusBar.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private ToolStrip toolbar;
    private ToolStripButton buttonSave;
    private ToolStripButton buttonNew;
    private ToolStripButton buttonOpen;
    private ToolStripButton buttonCopy;
    private ToolStripButton buttonPaste;
    private ToolStripButton buttonCut;
    private ToolStripButton buttonCompile;
    private ToolStripButton buttonTeam;
    private Splitter splitter;
    private TextBox messageArea;
    private StatusStrip statusBar;
    private ToolStripStatusLabel statusBarLabel;
    private RichTextBox editor;
    private Panel panelLineNumbers;
}
