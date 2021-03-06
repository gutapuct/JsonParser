﻿using System;
using System.Windows.Forms;
namespace EPAM.CSCourse2016.AlyoshkinZakhar.JsonParserUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Push_Click(object sender, EventArgs e)
        {
            var parser = new JsonZakhar();

            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            var result = parser.ToTestString(Input.Text);
            timer.Stop();
            
            Output.Text = result + "\r\n\r\n\r\nElapsedTicks: " + timer.ElapsedTicks;
        }

        private void example1_Click(object sender, EventArgs e)
        {
            Input.Text = "true";
            Output.Text = new JsonZakhar().ToTestString(Input.Text);
        }

        private void example2_Click(object sender, EventArgs e)
        {
            Input.Text = "[1, 2, 3, \"message\", true, null, [\"one\", \"two\", \"three\"], 15]";
            Output.Text = new JsonZakhar().ToTestString(Input.Text);
        }

        private void example3_Click(object sender, EventArgs e)
        {
            Input.Text = @"{""FirstName"":""Ivan"",""LastName"":""Ivanov""}";
            Output.Text = new JsonZakhar().ToTestString(Input.Text);
        }

        private void example4_Click(object sender, EventArgs e)
        {
            Input.Text = @"[{""FirstName"":""Ivan"",""LastName"":""Ivanov""},{""FirstName"":""Petr"",""LastName"":""Petrov""}]";
            Output.Text = new JsonZakhar().ToTestString(Input.Text);
        }

        private void example5_Click(object sender, EventArgs e)
        {
            Input.Text = "null";
            Output.Text = new JsonZakhar().ToTestString(Input.Text);
        }

        private void example6_Click(object sender, EventArgs e)
        {
            Input.Text = @"[{""Author"":""Ivanov"",""Year"":""2015"",""Text"":""Text-1""},{""Author"":""Petrov"",""Year"":""2016"",""Text"":""Text-2""},{""Author"":""Sidorov"",""Year"":""2007"",""Text"":""Text-3""}]";
            Output.Text = new JsonZakhar().ToTestString(Input.Text);
        }

        private void example7_Click(object sender, EventArgs e)
        {
            Input.Text = @"[{""FullName"":{""LastName"":""Ivanov"",""FirstName"":""Ivan""},""Age"":27,""IsMarried"":true,""CountChildren"":2},{""FullName"":{""LastName"":""Patrov"",""FirstName"":""Petr""},""Age"":16,""IsMarried"":false,""CountChildren"":null}]";
            Output.Text = new JsonZakhar().ToTestString(Input.Text);
        }
    }
}
