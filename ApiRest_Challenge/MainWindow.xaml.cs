using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace ApiRest_Challenge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            




        }


        private void Post_btn_Click(object sender, RoutedEventArgs e)
        {
            InfoTextBlock.Text = String.Empty;
            var client = new WebClient();
            var text = client.DownloadString($"https://jsonplaceholder.typicode.com/posts?userId={UserIdtextBox.Text}");
            ICollection<Post> posts = JsonConvert.DeserializeObject<ICollection<Post>>(text);

            foreach (Post post in posts)
            {
                InfoTextBlock.Text += post.Id.ToString()+ "\n " + post.Title + "\n" + post.Body + "\n";
            }


        }

        private void Comments_btn_Click(object sender, RoutedEventArgs e)
        {
            InfoTextBlock.Text = String.Empty;
            ICollection<Comment> comments = GetAllCommentsByPostId();
            foreach(Comment comment in comments)
            {
                InfoTextBlock.Text += comment.Id.ToString() + "\n " + comment.Name + "\n" + comment.Body + "\n";
            }

        }

        private ICollection<Comment> GetAllCommentsByPostId()
        {
            var client = new WebClient();
            var text = client.DownloadString($"https://jsonplaceholder.typicode.com/posts?userId={UserIdtextBox.Text}");
            ICollection<Post> posts = JsonConvert.DeserializeObject<ICollection<Post>>(text);
            List<Comment> comments = new List<Comment>();

            foreach (Post post in posts)
            {
                var comment = client.DownloadString($"https://jsonplaceholder.typicode.com/comments?postId={post.Id}");
                comments.AddRange(JsonConvert.DeserializeObject<ICollection<Comment>>(comment));
               
            }
            return comments;

        }

        private void Photos_btn_Click(object sender, RoutedEventArgs e)
        {
            InfoTextBlock.Text = String.Empty;
            ICollection<Photo> photos = GetAllPhotoByAlbumId();
            foreach (Photo photo in photos)
            {
                InfoTextBlock.Text += photo.Id.ToString() + "\n " + photo.Title + "\n";
            }

        }

        private ICollection<Photo> GetAllPhotoByAlbumId()
        {
            var client = new WebClient();
            var text = client.DownloadString($"https://jsonplaceholder.typicode.com/albums?userId={UserIdtextBox.Text}");
            ICollection<Album> albums = JsonConvert.DeserializeObject<ICollection<Album>>(text);
            List<Photo> photos = new List<Photo>();

            foreach(Album album in albums)
            {
                var photo = client.DownloadString($"https://jsonplaceholder.typicode.com/photos?albumId={album.Id}");
                photos.AddRange(JsonConvert.DeserializeObject<ICollection<Photo>>(photo));

            }
            return photos;

        }


    }
}
