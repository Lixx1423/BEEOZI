using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace BruceEverestEmporiumOfZombieInformation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IFirebaseConfig config = new FirebaseConfig {

            AuthSecret = "C9lRQWbDS9bPe9hJKlHNle7CuvR2gkaQhHIcMGuY",
            BasePath = "https://beeozi-default-rtdb.firebaseio.com/"

        };
        IFirebaseClient client;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void MainWindow_load(object sender, EventArgs e)
        {
            try
            {

                client = new FireSharp.FirebaseClient(config);
                if(client != null)
                {
                    MessageBox.Show("Connection Success");
                }

            }
            catch
            {
                MessageBox.Show("Connection Terminated");
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            Zombies zomb = new Zombies()
            {

                Name = name.Text,
                Age = age.Text,
                Type = type.Text,
                Ablity = ability.Text,
                LastKnown = lastknown.Text,
                EatBrains = eatbrains.IsEnabled,
            };

            FirebaseResponse response = client.Set("Zombies/" + name.Text, zomb);
            MessageBox.Show("Save Success");
        }


        private void update_Click(object sender, EventArgs e)
        {

            var zomb = new Zombies
            {
                Name = name.Text,
                Age = age.Text,
                Type = type.Text,
                Ablity = ability.Text,
                LastKnown = lastknown.Text,
                EatBrains = eatbrains.IsEnabled,


            };

            FirebaseResponse response = client.Update("Zombies/" + name.Text, zomb);
            MessageBox.Show("Data Update Success");
            name.Text = string.Empty;
            age.Text = string.Empty;
            type.Text = string.Empty;
            ability.Text = string.Empty;
            lastknown.Text = string.Empty;
            eatbrains.IsChecked = false;
        }


        private void retrieve_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = client.Get("Zombies/" + name.Text);
            Zombies zomb = response.ResultAs<Zombies>();


            if (name.Text.Equals(zomb.Name))
            {
                name.Text = zomb.Name;
                age.Text = zomb.Age;
                type.Text = zomb.Type;
                ability.Text = zomb.Ablity;
                lastknown.Text = zomb.LastKnown;
                eatbrains.IsChecked = zomb.EatBrains;
                MessageBox.Show("Data Found");
            }
            else
            {
                MessageBox.Show("Zombie not found in database :/");
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = client.Delete("Zombies/" + name.Text);
            MessageBox.Show("Delete Success");
        }
    }
}