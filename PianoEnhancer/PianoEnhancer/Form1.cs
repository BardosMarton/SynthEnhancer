using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PianoEnhancer
{
    public partial class Form1 : Form
    {
        Timer ticker = new Timer();

        Timer beatflash = new Timer();

        private List<int> recording = new List<int>();

        private List<int> major = new List<int>() { 0, 2, 4, 5, 7, 9, 11 };

        private List<int> minor = new List<int>() { 0, 2, 3, 5, 7, 8, 10 };

        Dictionary<Button, int> noteDict = new Dictionary<Button, int>();

        private List<int> allowedKeys;

        private bool enhance = false;



        System.Windows.Media.MediaPlayer c1p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer cs1p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer d1p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer ds1p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer e1p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer f1p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer fs1p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer g1p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer gs1p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer a1p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer as1p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer b1p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer c2p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer cs2p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer d2p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer ds2p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer e2p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer f2p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer fs2p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer g2p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer gs2p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer a2p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer as2p = new System.Windows.Media.MediaPlayer();

        System.Windows.Media.MediaPlayer b2p = new System.Windows.Media.MediaPlayer();

        private void NotesToDict()
        {
            noteDict.Add(c1, 0);
            noteDict.Add(cs1, 1);
            noteDict.Add(d1, 2);
            noteDict.Add(ds1, 3);
            noteDict.Add(e1, 4);
            noteDict.Add(f1, 5);
            noteDict.Add(fs1, 6);
            noteDict.Add(g1, 7);
            noteDict.Add(gs1, 8);
            noteDict.Add(a1, 9);
            noteDict.Add(as1, 10);
            noteDict.Add(b1, 11);
            noteDict.Add(c2, 12);
            noteDict.Add(cs2, 13);
            noteDict.Add(d2, 14);
            noteDict.Add(ds2, 15);
            noteDict.Add(e2, 16);
            noteDict.Add(f2, 17);
            noteDict.Add(fs2, 18);
            noteDict.Add(g2, 19);
            noteDict.Add(gs2, 20);
            noteDict.Add(a2, 21);
            noteDict.Add(as2, 22);
            noteDict.Add(b2, 23);
        }
        
        public Form1()
        {
            InitializeComponent();
            NotesToDict();
            ticker.Interval = 1000;
            ticker.Tick += TickEvent;
            BPMbox.Text = "60";

            beatflash.Interval = 60000 / (bpm * 4);
            beatflash.Tick += FlashTick;
            beatflash.Start();
            ticker.Start();
        }

        

        private void SelectedKey(int stepup)
        {
            for (int i = 0; i < allowedKeys.Count; i++)
            {
                allowedKeys[i] += stepup;

                if (allowedKeys[i] > 23)
                    allowedKeys[i] -= 24;
            }
        }

        bool keyBeenChanged = false;

        private void ChangeKey_Click(object sender, EventArgs e)
        {
            keyBeenChanged = true;

            if (scale.SelectedItem.ToString() == "[none]" || key.SelectedItem.ToString() == "[none]")
            {
                checkEnhance.Checked = false;
            }

            allowedKeys = new List<int>();

            foreach (var keybutton in noteDict)
            {
                if ((keybutton.Key.Name.ToString())[1] == 's')
                    keybutton.Key.BackColor = Color.Black;
                else
                {
                    keybutton.Key.BackColor = Color.White;
                }

                keybutton.Key.Enabled = true;
            }

            if (scale.SelectedItem != null && key.SelectedItem != null && scale.SelectedItem.ToString() != "[none]" && key.SelectedItem.ToString() != "[none]")
            {
                string selectedScale = scale.SelectedItem.ToString();
                string selectedKey = key.SelectedItem.ToString();

                if (selectedScale == "Major")
                {
                    for (int i = 0; i < major.Count; i++)
                    {
                        allowedKeys.Add(major[i]);
                    }
                    for (int i = 0; i < major.Count; i++)
                    {
                        allowedKeys.Add(major[i] + 12);
                    }
                }
                else if (selectedScale == "Minor")
                {
                    for (int i = 0; i < minor.Count; i++)
                    {
                        allowedKeys.Add(minor[i]);
                    }
                    for (int i = 0; i < minor.Count; i++)
                    {
                        allowedKeys.Add(minor[i] + 12);
                    }
                }
                
                if (selectedKey == "C#")
                {
                    SelectedKey(1);
                }
                else if (selectedKey == "D")
                {
                    SelectedKey(2);
                }
                else if (selectedKey == "D#")
                {
                    SelectedKey(3);
                }
                else if (selectedKey == "E")
                {
                    SelectedKey(4);
                }
                else if (selectedKey == "F")
                {
                    SelectedKey(5);
                }
                else if (selectedKey == "F#")
                {
                    SelectedKey(6);
                }
                else if (selectedKey == "G")
                {
                    SelectedKey(7);
                }
                else if (selectedKey == "G#")
                {
                    SelectedKey(8);
                }
                else if (selectedKey == "A")
                {
                    SelectedKey(9);
                }
                else if (selectedKey == "A#")
                {
                    SelectedKey(10);
                }
                else if (selectedKey == "B")
                {
                    SelectedKey(11);
                }



                foreach (var item in noteDict)
                {
                    //note bgcolor change gray
                    //note inactive

                    if ((item.Key.Name.ToString())[1] == 's')
                        item.Key.BackColor = Color.FromArgb(0x204F4F4F);
                    else
                        item.Key.BackColor = Color.FromArgb(0x10FFFFFF);

                    item.Key.Enabled = false;

                    foreach (int allowed in allowedKeys)
                    {
                        if (item.Value == allowed)
                        {
                            //note bgcolor change normal
                            //note active

                            if ((item.Key.Name.ToString())[1] == 's')
                                item.Key.BackColor = Color.Black;
                            else
                            {
                                item.Key.BackColor = Color.White;
                            }

                            item.Key.Enabled = true;

                            break;
                        }
                    }
                }


            }
        }

        private void OrderKeys()
        {
            for (int i = 1; i < allowedKeys.Count; i++)
            {
                while (i > 0 && !(allowedKeys[i - 1] < allowedKeys[i]))
                {
                    int temp = allowedKeys[i - 1];
                    allowedKeys[i - 1] = allowedKeys[i];
                    allowedKeys[i] = temp;


                    i--;
                }
            }
        }

        Random rnd = new Random();

        private void RandomAccord(int i)
        {
            int random = rnd.Next(0, 4);

            if (random == 0)
            {
                recording.Add(allowedKeys[i + 3]);
                recording.Add(allowedKeys[i + 5]);
                recording.Add(allowedKeys[i + 7]);
            }
            else if (random == 1)
            {
                recording.Add(allowedKeys[i + 2]);
                recording.Add(allowedKeys[i + 5]);
                recording.Add(allowedKeys[i + 7]);
            }
            else if (random == 2)
            {
                recording.Add(allowedKeys[i + 2]);
                recording.Add(allowedKeys[i + 4]);
                recording.Add(allowedKeys[i + 7]);
            }
            else
            {
                recording.Add(allowedKeys[i + 2]);
                recording.Add(allowedKeys[i + 4]);
                recording.Add(allowedKeys[i + 6]);
            }
        }

        private void RandomAccordHigh(int i)
        {
            int random = rnd.Next(0, 4);

            if (random == 0)
            {
                recording.Add(allowedKeys[i - 2]);
                recording.Add(allowedKeys[i - 4]);
                recording.Add(allowedKeys[i - 7]);
            }
            else if (random == 1)
            {
                recording.Add(allowedKeys[i - 2]);
                recording.Add(allowedKeys[i - 5]);
                recording.Add(allowedKeys[i - 7]);
            }
            else if (random == 2)
            {
                recording.Add(allowedKeys[i - 3]);
                recording.Add(allowedKeys[i - 5]);
                recording.Add(allowedKeys[i - 7]);
            }
            else
            {
                recording.Add(allowedKeys[i - 2]);
                recording.Add(allowedKeys[i - 4]);
                recording.Add(allowedKeys[i - 6]);
            }
        }

        private void EnhanceNotes(int rootNote)
        {
            if (enhance == true)
            {
                OrderKeys();

                if (scale.SelectedItem.ToString() == "Major")
                {
                    if (rootNote < 12)
                    {
                        for (int i = 0; i < allowedKeys.Count; i++)
                        {
                            if (allowedKeys[i] == rootNote)
                            {
                                RandomAccord(i);
                                break;
                            }
                        }
                    }
                    else if (rootNote > 11)
                    {
                        for (int i = 0; i < allowedKeys.Count; i++)
                        {
                            if (allowedKeys[i] == rootNote)
                            {
                                RandomAccordHigh(i);
                                break;
                            }
                        }
                    }
                }
                else if (scale.SelectedItem.ToString() == "Minor")
                {
                    if (rootNote < 12)
                    {
                        for (int i = 0; i < allowedKeys.Count; i++)
                        {
                            if (allowedKeys[i] == rootNote)
                            {
                                RandomAccord(i);
                                break;
                            }
                        }
                    }
                    else if (rootNote > 11)
                    {
                        for (int i = 0; i < allowedKeys.Count; i++)
                        {
                            if (allowedKeys[i] == rootNote)
                            {
                                RandomAccordHigh(i);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void C1_Click(object sender, EventArgs e)
        {
            c1p.Open(new System.Uri(@"c1.wav", UriKind.Relative));
            c1p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(0);

                EnhanceNotes(0);
            }
        }

        private void Cs1_Click(object sender, EventArgs e)
        {
            cs1p.Open(new System.Uri(@"cs1.wav", UriKind.Relative));
            cs1p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(1);

                EnhanceNotes(1);
            }
        }

        private void D1_Click(object sender, EventArgs e)
        {
            d1p.Open(new System.Uri(@"d1.wav", UriKind.Relative));
            d1p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(2);

                EnhanceNotes(2);
            }
        }

        private void Ds1_Click(object sender, EventArgs e)
        {
            ds1p.Open(new System.Uri(@"ds1.wav", UriKind.Relative));
            ds1p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(3);

                EnhanceNotes(3);
            }
        }

        private void E1_Click(object sender, EventArgs e)
        {
            e1p.Open(new System.Uri(@"e1.wav", UriKind.Relative));
            e1p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(4);

                EnhanceNotes(4);
            }
        }

        private void F1_Click(object sender, EventArgs e)
        {
            f1p.Open(new System.Uri(@"f1.wav", UriKind.Relative));
            f1p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(5);

                EnhanceNotes(5);
            }
        }

        private void Fs1_Click(object sender, EventArgs e)
        {
            fs1p.Open(new System.Uri(@"fs1.wav", UriKind.Relative));
            fs1p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(6);

                EnhanceNotes(6);
            }
        }

        private void G1_Click(object sender, EventArgs e)
        {
            g1p.Open(new System.Uri(@"g1.wav", UriKind.Relative));
            g1p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(7);

                EnhanceNotes(7);
            }
        }

        private void Gs1_Click(object sender, EventArgs e)
        {
            gs1p.Open(new System.Uri(@"gs1.wav", UriKind.Relative));
            gs1p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(8);

                EnhanceNotes(8);
            }
        }

        private void A1_Click(object sender, EventArgs e)
        {
            a1p.Open(new System.Uri(@"a1.wav", UriKind.Relative));
            a1p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(9);

                EnhanceNotes(9);
            }
        }

        private void As1_Click(object sender, EventArgs e)
        {
            as1p.Open(new System.Uri(@"as1.wav", UriKind.Relative));
            as1p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(10);

                EnhanceNotes(10);
            }
        }

        private void B1_Click(object sender, EventArgs e)
        {
            b1p.Open(new System.Uri(@"b1.wav", UriKind.Relative));
            b1p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(11);

                EnhanceNotes(11);
            }
        }

        private void C2_Click(object sender, EventArgs e)
        {
            c2p.Open(new System.Uri(@"c2.wav", UriKind.Relative));
            c2p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(12);

                EnhanceNotes(12);
            }
        }

        private void Cs2_Click(object sender, EventArgs e)
        {
            cs2p.Open(new System.Uri(@"cs2.wav", UriKind.Relative));
            cs2p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(13);

                EnhanceNotes(13);
            }
        }

        private void D2_Click(object sender, EventArgs e)
        {
            d2p.Open(new System.Uri(@"d2.wav", UriKind.Relative));
            d2p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(14);

                EnhanceNotes(14);
            }
        }

        private void Ds2_Click(object sender, EventArgs e)
        {
            ds2p.Open(new System.Uri(@"ds2.wav", UriKind.Relative));
            ds2p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(15);

                EnhanceNotes(15);
            }
        }

        private void E2_Click(object sender, EventArgs e)
        {
            e2p.Open(new System.Uri(@"e2.wav", UriKind.Relative));
            e2p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(16);

                EnhanceNotes(16);
            }
        }

        private void F2_Click(object sender, EventArgs e)
        {
            f2p.Open(new System.Uri(@"f2.wav", UriKind.Relative));
            f2p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(17);

                EnhanceNotes(17);
            }
        }

        private void Fs2_Click(object sender, EventArgs e)
        {
            fs2p.Open(new System.Uri(@"fs2.wav", UriKind.Relative));
            fs2p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(18);

                EnhanceNotes(18);
            }
        }

        private void G2_Click(object sender, EventArgs e)
        {
            g2p.Open(new System.Uri(@"g2.wav", UriKind.Relative));
            g2p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(19);

                EnhanceNotes(19);
            }
        }

        private void Gs2_Click(object sender, EventArgs e)
        {
            gs2p.Open(new System.Uri(@"gs2.wav", UriKind.Relative));
            gs2p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(20);

                EnhanceNotes(20);
            }
        }

        private void A2_Click(object sender, EventArgs e)
        {
            a2p.Open(new System.Uri(@"a2.wav", UriKind.Relative));
            a2p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(21);

                EnhanceNotes(21);
            }
        }

        private void As2_Click(object sender, EventArgs e)
        {
            as2p.Open(new System.Uri(@"as2.wav", UriKind.Relative));
            as2p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(22);

                EnhanceNotes(22);
            }
        }

        private void B2_Click(object sender, EventArgs e)
        {
            b2p.Open(new System.Uri(@"b2.wav", UriKind.Relative));
            b2p.Play();

            if (recRadioButton.Checked == true)
            {
                recording.Add(23);

                EnhanceNotes(23);
            }
        }

        private void LabelKey_MouseDown(object sender, MouseEventArgs e)
        {
            labelKey.Visible = false;
            key.DroppedDown = true;
        }

        private void LabelScale_MouseDown(object sender, MouseEventArgs e)
        {
            labelScale.Visible = false;
            scale.DroppedDown = true;
        }

        private void Key_DropDownClosed(object sender, EventArgs e)
        {
            if (key.SelectedIndex == -1)
            {
                labelKey.Visible = true;
            }
        }

        private void Scale_DropDownClosed(object sender, EventArgs e)
        {
            if (scale.SelectedIndex == -1)
            {
                labelScale.Visible = true;
            }
        }

        private void Key_DropDown(object sender, EventArgs e)
        {
            labelKey.Visible = false;
        }

        private void Scale_DropDown(object sender, EventArgs e)
        {
            labelScale.Visible = false;
        }

        int t = 0;

        private void GoodKey()
        {
            foreach (var item in noteDict)
            {
                if (item.Value == recording[t])
                {
                    if (item.Key.Name.ToString() == "c1")
                    {
                        C1_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "cs1")
                    {
                        Cs1_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "d1")
                    {
                        D1_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "ds1")
                    {
                        Ds1_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "e1")
                    {
                        E1_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "f1")
                    {
                        F1_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "fs1")
                    {
                        Fs1_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "g1")
                    {
                        G1_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "gs1")
                    {
                        Gs1_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "a1")
                    {
                        A1_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "as1")
                    {
                        As1_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "b1")
                    {
                        B1_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "c2")
                    {
                        C2_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "cs2")
                    {
                        Cs2_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "d2")
                    {
                        D2_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "ds2")
                    {
                        Ds2_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "e2")
                    {
                        E2_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "f2")
                    {
                        F2_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "fs2")
                    {
                        Fs2_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "g2")
                    {
                        G2_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "gs2")
                    {
                        Gs2_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "a2")
                    {
                        A2_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "as2")
                    {
                        As2_Click(null, null);
                    }
                    if (item.Key.Name.ToString() == "b2")
                    {
                        B2_Click(null, null);
                    }
                }
            }
        }


        private void TickEvent(object sender, EventArgs e)
        {
            if (playRadioButton.Checked == true && t < recording.Count)
            {
                if (enhance == true)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        GoodKey();
                        t++;
                    }
                }
                else
                {
                    GoodKey();
                    t++;
                }
            }
            if (playRadioButton.Checked == true && t >= recording.Count)
            {
                recording = new List<int>();

                freeRadioButton.Checked = true;

                t = 0;
            }
        }

        private void PlayRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (playRadioButton.Checked == true)
                checkEnhance.Enabled = false;
            //disable enhance button

            else if (playRadioButton.Checked == false)
            {
                checkEnhance.Enabled = true;
                BeatBox.Visible = false;
            }
            //enable
        }

        private void RecRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (recRadioButton.Checked == true)
                recording = new List<int>();
        }

        int bpm = 60;

        private void BPMbox_TextChanged(object sender, EventArgs e)
        {
            string str = BPMbox.Text;

            try
            {
                bpm = int.Parse(str);
            }
            catch
            {
                bpm = 60;
                BPMbox.Text = "60";
                ticker.Interval = 1000;
            }

            if (bpm > 0 && bpm < 601)
            {
                ticker.Interval = 60000 / bpm;
            }
        }

        private void BPMbox_Leave(object sender, EventArgs e)
        {
            if (bpm < 1 || bpm > 600)
            {
                bpm = 60;
                BPMbox.Text = "60";
                ticker.Interval = 1000;
            }
        }

        private void CheckEnhance_CheckedChanged(object sender, EventArgs e)
        {
            enhance = false;


            if (keyBeenChanged == true)
            {
                if (checkEnhance.Checked == true)
                {
                    try
                    {
                        if (key.SelectedItem.ToString() != "[none]" && scale.SelectedItem.ToString() != "[none]" && key.SelectedItem != null && scale.SelectedItem != null)
                            enhance = true;
                        else
                            checkEnhance.Checked = false;
                    }
                    catch
                    {
                        checkEnhance.Checked = false;
                    }
                }
            }
            else
                checkEnhance.Checked = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Image background = new Bitmap(@"FormWallpaper.png");
            this.BackgroundImage = background;

            BeatBox.BackColor = Color.FromArgb(0x304F004F);

            BeatBox.Visible = false;
        }

        int tickcount = 0;

        private void FlashTick(object sender, EventArgs e)
        {
            if (playRadioButton.Checked == true)
            {
                beatflash.Interval = 60000 / (bpm * 4);

                if (tickcount == 0 || tickcount == 2)
                {
                    BeatBox.BackColor = Color.FromArgb(0x304F004F);

                    BeatBox.Visible = true;

                    tickcount += 1;
                }
                else if (tickcount == 1)
                {
                    BeatBox.BackColor = Color.FromArgb(0x504F004F);

                    tickcount += 1;
                }
                else
                {
                    BeatBox.Visible = false;

                    tickcount = 0;
                }
            }
        }
    }
}
