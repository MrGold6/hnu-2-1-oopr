using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using static MaterialSkin.Controls.MaterialForm;
using MaterialSkin;
using MaterialSkin.Controls;

namespace lab2
{
    public partial class Form1 : Form
    {
        //10. Створити масив, що містить вказівники на об’єкти цих класів.
        Voucher[] vouchers;
        University university;
        public Form1()
        {
            InitializeComponent();
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.DeepPurple400, Primary.DeepPurple500,
                Primary.DeepPurple500, Accent.Indigo200,
                TextShade.WHITE
                );

        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (File.Exists("Uni.dat"))
            {

                university = University.LoadFromFile("Uni.dat");
            }

            for (int i = 0; i < vouchers.Length; i++)
            {
                if (string.Equals(vouchers[i].country, comboBox1.SelectedItem))
                {
             
                    Student student = new Student();

                    student.surname = materialSingleLineTextField1.Text;
                    student.name = materialSingleLineTextField2.Text;
                    student.lastname = materialSingleLineTextField3.Text;
                    student.age = Int32.Parse(materialSingleLineTextField4.Text);

                    student.course = Int32.Parse(materialSingleLineTextField5.Text);
                    student.group = materialSingleLineTextField6.Text;
                    student.speciality = materialSingleLineTextField7.Text;

                    student.voucher = vouchers[i];

                    university.tradeUnion.addStudent(student);

                    
                }
         
            }

            university.writeToFile("Uni.dat");

            richTextBox1.Text = "";
            richTextBox1.Text += "Університет: " + university.name + "\r\n";
            richTextBox1.Text += "Профспілка: " + university.tradeUnion.name + "\r\n";

            //richTextBox1.Text += "Студенти, що обрали ваучер " + vouchers[i].country + "\r\n";

            for (int j = 0; j < university.tradeUnion.students.Count; j++)
            {
                richTextBox1.Text += "Студент: " + university.tradeUnion.students[j].surname
                        +" " + university.tradeUnion.students[j].name
                        +" " + university.tradeUnion.students[j].group
                        +" " + university.tradeUnion.students[j].course
                        +" " + university.tradeUnion.students[j].speciality
                        +"- ваучер: " + university.tradeUnion.students[j].voucher.country
                    + "\r\n";
            }
            richTextBox1.Text += "\r\n";
            


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Student head = new Student("2030040", "Nastya", "Bin", "Oleksandrivna", 18, "head@gmail.com", "058938593", "Nov, 9",
                "19.11.2023", false, "School 5", 883, "zal3", "KI2c-22-2", "computer engeneering", 4);
            Human rector = new Human("930285", "Sem", "Vog", "Pitterson", 40, "rector@gmail.com", "324238513", "Vod, 10");

           university = new University("HNU", 10, "093827930", true, rector,
                 "Profspilca2", 10, "02.02.2003", head, true, "doc");

            //10. Створити масив, що містить вказівники на об’єкти цих класів.
            vouchers = new Voucher[3];

            vouchers[0] = new Voucher("America", "New York", "11.11.2023", 300, 223, "AOK", true, "bus");
            vouchers[1] = new Voucher("France", "Strasburg", "01.05.2023", 221, 332, "AOK", true, "plane");
            vouchers[2] = new Voucher("Germany", "Dresden", "04.07.2023", 392, 121, "AOK", false, "train");


            university.tradeUnion.addVoucher(vouchers[0]);
            university.tradeUnion.addVoucher(vouchers[1]);
            university.tradeUnion.addVoucher(vouchers[2]);

            for (int i = 0; i < vouchers.Length; i++)
            {
                comboBox1.Items.Add(vouchers[i].country);
            }
            comboBox1.SelectedItem = vouchers[0].country;


        }

        private void materialLabel3_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel7_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
    }


    [Serializable]
    class Voucher
    {
        //3. Реалізувати поля даних за допомогою властивостей (get, set).
        public int code { get; set; }
        public string insuranse { get; set; }
        public bool hasSpecialStaff { get; set; }
        public string transport { get; set; }
        public string city { get; set; }

        public string country { get; set; }
        public string date { get; set; }
        public int cost { get; set; }

        //4. Кожен клас містить конструктами без параметрів, з параметрами (базовий та похідні класи повинні містити не менше п’яти методів).
        public Voucher() { }
        public Voucher(string country, string city, string date, int cost, int code, string insuranse, bool hasSpecialStaff, string transport)
        {
            this.country = country;
            this.city = city;
            this.date = date;
            this.cost = cost;
            this.code = code;
            this.insuranse = insuranse;
            this.hasSpecialStaff = hasSpecialStaff;
            this.transport = transport;
        }
        public Voucher(string country, string city, string date)
        {
            this.country = country;
            this.city = city;
            this.date = date;
        }
        public void Copy(Voucher v)
        {
            this.country = v.country;
            this.city = v.city;
            this.date = v.date;
            this.cost = v.cost;
            this.code = v.code;
            this.insuranse = v.insuranse;
            this.hasSpecialStaff = v.hasSpecialStaff;
            this.transport = v.transport;
        }
        public Voucher getCopy()
        {
            return new Voucher(country, city, date, cost, code, insuranse, hasSpecialStaff, transport);
        }


        //11. Визначити в класі метод, який записує у файл та читає з файлу поля даних класу.
        public void writeToFile(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, this);
            }
        }
        public static Voucher LoadFromFile(string filePath)
        {
            Voucher loadedData = null;

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                loadedData = (Voucher)formatter.Deserialize(fileStream);
            }

            return loadedData;
        }
    }

    [Serializable]
    public class Human
    {
        //3. Реалізувати поля даних за допомогою властивостей (get, set).
        public string RNTRC { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string lastname { get; set; }
        public int age { get; set; }

        public string email { get; set; }
        public string phone_number { get; set; }
        public string address { get; set; }

        //4. Кожен клас містить конструктами без параметрів, з параметрами (базовий та похідні класи повинні містити не менше п’яти методів).
        public Human() { }
        public Human(string RNTRC, string name, string surname, string lastname, int age, string email, string phone_number, string address)
        {
            this.RNTRC = RNTRC;
            this.name = name;
            this.surname = surname;
            this.lastname = lastname;
            this.age = age;
            this.email = email;
            this.phone_number = phone_number;
            this.address = address;
        }
        public void Copy(Human h)
        {
            this.RNTRC = h.RNTRC;
            this.name = h.name;
            this.surname = h.surname;
            this.lastname = h.lastname;
            this.age = h.age;
            this.email = h.email;
            this.phone_number = h.phone_number;
            this.address = h.address;
        }
        public Human getCopy()
        {
            return new Human(RNTRC, name, surname, lastname, age, email, phone_number, address);
        }

        //9. Кожен клас, що входить в ієрархію, що використовує відкрите наслідування, повинен містити принаймні одну віртуальну функцію. Функціональність зазначених функцій не повинна дублюватися.
        public virtual string getInfo()
        {
            return "Human:" + RNTRC+ 
                " " + name + 
                " " + surname
                + " " + lastname
                + " " + age
                + " " + email 
                + " " + phone_number
                + " " + address;
        }

        public virtual void changeAge()
        {
            this.age++;
        }



        //11. Визначити в класі метод, який записує у файл та читає з файлу поля даних класу.
        public void writeToFile(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, this);
            }
        }
        public static Human LoadFromFile(string filePath)
        {
            Human loadedData = null;

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                loadedData = (Human)formatter.Deserialize(fileStream);
            }

            return loadedData;
        }

    };

    [Serializable]
    class Student : Human
    {
        //3. Реалізувати поля даних за допомогою властивостей (get, set).
        public string date_of_admission { get; set; }
        public bool isGraduated { get; set; }
        public string atestat { get; set; }
        public int number_of_record_book { get; set; }
        public string document { get; set; }
        public Voucher voucher { get; set; }

        public string group { get; set; }
        public string speciality { get; set; }
        public int course { get; set; }

        //5. Продемонструвати приховування успадкованого члена за допомогою модифікатора new.
        new protected string name { get; set; }


        //4. Кожен клас містить конструктами без параметрів, з параметрами (базовий та похідні класи повинні містити не менше п’яти методів).
        public Student() { }
        //6. Продемонструвати доступу до членів базового з похідного класу за допомогою слова base.
        public Student(string RNTRC, string name, string surname, string lastname, int age, string email,
            string phone_number, string address, string date_of_admission, bool isGraduated, string atestat, 
            int number_of_record_book, string document, string group, string speciality, int course) 
            : base (RNTRC, name, surname, lastname, age, email, phone_number, address)
        {

            this.date_of_admission = date_of_admission;
            this.isGraduated = isGraduated;
            this.atestat = atestat;
            this.number_of_record_book = number_of_record_book;
            this.document = document;
            this.group = group;
            this.speciality = speciality;
            this.course = course;
        }
        public void Copy(Student h)
        {
            this.name = h.name;
            this.surname = h.surname;
            this.lastname = h.lastname;
            this.age = h.age;
            this.email = h.email;
            this.phone_number = h.phone_number;
            this.address = h.address;

            this.date_of_admission = h.date_of_admission;
            this.isGraduated = h.isGraduated;
            this.atestat = h.atestat;
            this.number_of_record_book = h.number_of_record_book;
            this.document = h.document;
            this.group = h.group;
            this.speciality = h.speciality;
            this.course = h.course;
        }

        public Student getCopy()
        {
            return new Student(RNTRC, name, surname, lastname, age, email, phone_number, address, 
                date_of_admission, isGraduated, atestat, number_of_record_book, document, group, speciality, 
                course);
        }

        //todo: ???
        //7. Продемонструвати застосуванні до класу модифікатора sealed для заборони іншим класам успадковувати від цього класу, а також показати дозвіл успадковувати класам від визначеного Вами класу, забороняючи їм при цьому перевизначати визначені віртуальні методи.
        public sealed override string getInfo()
        {
            return "Student:" + RNTRC +
                " " + name +
                " " + surname
                + " " + lastname
                + " " + age
                + " " + email
                + " " + phone_number
                + " " + address;
        }

        //8. Надати нову реалізацію члена, успадкованого від базового класу за допомогою override.
        public override void changeAge()
        {
            this.age+=2;
        }

        //11. Визначити в класі метод, який записує у файл та читає з файлу поля даних класу.
        public void writeToFile(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, this);
            }
        }
        public static Student LoadFromFile(string filePath)
        {
            Student loadedData = null;

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                loadedData = (Student)formatter.Deserialize(fileStream);
            }

            return loadedData;
        }

    }

    [Serializable]
    class TradeUnion
    {
        //3. Реалізувати поля даних за допомогою властивостей (get, set).
        public Human head { get; set; }
        public List<Voucher> vouchers = new List<Voucher>();
        public List<Student> students = new List<Student>();
        public string document { get; set; }
        public bool isApproved { get; set; }


        public string name { get; set; }
        public int count  { get; set; }
        public string dateOfCreation { get; set; }

        //4. Кожен клас містить конструктами без параметрів, з параметрами (базовий та похідні класи повинні містити не менше п’яти методів).
        public TradeUnion() { }
        public TradeUnion(string name, int count, string dateOfCreation, Human head, bool isApproved)
        {
            this.name = name;
            this.count = count;
            this.dateOfCreation = dateOfCreation;
            this.document = document;
            this.isApproved = isApproved;
            this.head = head;
        }

        public void increaseCount()
        {
            this.count ++;
        }

        public void decreaseCount()
        {
            this.count--;
        }

        public void addStudent(Student student)
        {
            this.students.Add(student);
        }

        public void addVoucher(Voucher voucher)
        {
            this.vouchers.Add(voucher);
        }

        //11. Визначити в класі метод, який записує у файл та читає з файлу поля даних класу.
        public void writeToFile(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, this);
            }
        }
        public static TradeUnion LoadFromFile(string filePath)
        {
            TradeUnion loadedData = null;

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                loadedData = (TradeUnion)formatter.Deserialize(fileStream);
            }

            return loadedData;
        }

    }

    [Serializable]
    class University
    {
        //3. Реалізувати поля даних за допомогою властивостей (get, set).
        public Human rector { get; set; }
        public List<Student> students { get; set; }
        public int count { get; set; }
        public bool isCertified { get; set; }
        public string phone_number { get; set; }
        public string name { get; set; }
        public static string dateOfCreation { get; set; }
        public TradeUnion tradeUnion = new TradeUnion();


        //4. Кожен клас містить конструктами без параметрів, з параметрами (базовий та похідні класи повинні містити не менше п’яти методів).
        public University() { }
        public University(string name, int count, Human rector)
        {
            this.name = name;
            this.count = count;
            this.rector = rector;
        }
        public University(string name, int count, string phone_number, bool isCertified,
            Human rector,
            string t_name, int t_count, string t_dateOfCreation, Human head, bool t_isApproved, string t_document) {
            this.name = name;
            this.count = count;
            this.phone_number = phone_number;
            this.isCertified = isCertified;
            this.rector = rector;
            this.students = students;

            this.tradeUnion.name = t_name;
            this.tradeUnion.count = t_count;
            this.tradeUnion.head = head;
            this.tradeUnion.dateOfCreation = t_dateOfCreation;
            this.tradeUnion.isApproved = t_isApproved;
            this.tradeUnion.document = t_document;
        }

        public void increaseCount()
        {
            this.count++;
        }

        public void decreaseCount()
        {
            this.count--;
        }

        public void addStudent(Student student)
        {
            this.students.Add(student);
        }


        //11. Визначити в класі метод, який записує у файл та читає з файлу поля даних класу.
        public void writeToFile(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, this);
            }
        }
        public static University LoadFromFile(string filePath)
        {
            University loadedData = null;

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                loadedData = (University)formatter.Deserialize(fileStream);
            }

            return loadedData;
        }

    }

    //12. Визначити в класі статичний компонент – вказівник на початок зв’язного списку об’єктів і статичний метод для перегляду списку. Статичний метод перегляду списку викликати не через об’єкт, а через клас. Дані записуються у файл.
    class LinkedListNode
    {
        public Voucher voucher { get; set; }
        public LinkedListNode Next { get; set; }

        public LinkedListNode(Voucher v)
        {
            voucher = v;
            Next = null;
        }
    }
    class LinkedList
    {
        private static LinkedListNode head;

        public static void AddNode(Voucher v)
        {
            LinkedListNode newNode = new LinkedListNode(v);
            newNode.Next = head;
            head = newNode;
        }

        public static void ViewListAndWriteToFile(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                LinkedListNode current = head;
                while (current != null)
                {
                    Console.WriteLine(current.voucher.country);
                    writer.WriteLine(current.voucher.country);
                    current = current.Next;
                }
            }
        }
    }

}

