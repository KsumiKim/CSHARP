using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Database.Step3.Data;

namespace Database.Step3.UnitTest
{
    [TestClass]
    public class DataAlbumTest
    {
        private AlbumData _data;

        [TestInitialize]
        public void Setup()
        {
            _data = new AlbumData();
        }

        [TestMethod]
        public void 아티스트아이디가_10보다_크고_타이틀이_b_로_시작하는_앨범아이디의_평균은_6이어야_함()
        {
            List<Album> albums = _data.Search(10, "b");

            int count = 0;
            int sum = 0;

            foreach (Album album in albums)
            {
                count++;
                sum += album.AlbumId;
            }

            Assert.AreEqual(6, sum / count);
        }

        [TestMethod()]
        public void GetByPKTest()
        {
            Album album = _data.GetByPK(347);
            Assert.AreEqual("Koyaanisqatsi (Soundtrack from the Motion Picture)", album.Title);
        }

        [TestMethod()]
        public void GetCountTest()
        {
            int count = _data.GetCount();
            Assert.AreEqual(347, count);
        }

        [TestMethod()]
        public void InsertTest()
        {
            int oldCount = _data.GetCount();

            Album album = new Album();
            album.Title = DateTime.Now.ToString();
            album.ArtistId = 1;

            _data.Insert(album);

            int newCount = _data.GetCount();

            Assert.AreEqual(oldCount + 1, newCount);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            string now = DateTime.Now.ToString();

            Album album = _data.GetByPK(1);
            Assert.AreNotEqual(now, album.Title);

            album.Title = now;
            _data.Update(album);

            album = _data.GetByPK(1);
            Assert.AreEqual(now, album.Title);
        }
    }
}
