using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace www.asm32.net
{
    /// <summary>
    /// 验证码生成工具，支持bmp字节数据生成 2017-12-01 by PASCAL asm32 gyl
    /// </summary>
    public class PaValidationCodeUtil
    {
        private string strValidationCode = null;

        /// <summary>
        /// 随机生成器
        /// </summary>
        private Random rnd = null;

        /// <summary>
        /// 验证码公共访问接口
        /// </summary>
        public string ValidationCode
        {
            get
            {
                if (this.strValidationCode == null)
                {
                    this.strValidationCode = this.GernateValidationCode();
                }
                return this.strValidationCode;
            }
            set
            {
                this.strValidationCode = value;
            }
        }

        /// <summary>
        /// 位掩码，用来检测对应的位有没有像素点
        /// </summary>
        public readonly byte[] ByteMask = { 128, 64, 32, 16, 8, 4, 2, 1 };
        public readonly byte[] ByteXorMask = { ~128 & 255, ~64 & 255, ~32 & 255, ~16 & 255, ~8 & 255, ~4 & 255, ~2 & 255, ~1 & 255 };

        /// <summary>
        /// 这些字符对应下面的点阵数据
        /// </summary>
        public const string AlphaTable = "0123456789";

        public const int MaxAlphaTable = 10;

        public readonly byte[,] charBytes = {
            //// 0
            { 0, 0, 56, 108, 198, 198, 214, 214, 198, 198, 108, 56, 0, 0, 0, 0},
            //// 1
            { 0, 0, 24, 56, 120, 24, 24, 24, 24, 24, 24, 126, 0, 0, 0, 0 },
            //// 2
            { 0, 0, 124, 198, 6, 12, 24, 48, 96, 192, 198, 254, 0, 0, 0, 0 },
            //// 3
            { 0, 0, 124, 198, 6, 6, 60, 6, 6, 6, 198, 124, 0, 0, 0, 0 },
            //// 4
            { 0, 0, 12, 28, 60, 108, 204, 254, 12, 12, 12, 30, 0, 0, 0, 0 },
            //// 5
            {0, 0, 254, 192, 192, 192, 252, 6, 6, 6, 198, 124, 0, 0, 0, 0},
            //// 6
            {0, 0, 56, 96, 192, 192, 252, 198, 198, 198, 198, 124, 0, 0, 0, 0},
            //// 7
            {0, 0, 254, 198, 6, 6, 12, 24, 48, 48, 48, 48, 0, 0, 0, 0},
            //// 8
            {0, 0, 124, 198, 198, 198, 124, 198, 198, 198, 198, 124, 0, 0, 0, 0},
            //// 9
            {0, 0, 124, 198, 198, 198, 126, 6, 6, 6, 12, 120, 0, 0, 0, 0}
        };

        private byte[] CreateEmptyBmpData()
        {
            //// 80 x 31 x 2 单色 bmp 数据
            return new byte[] {
                66, 77, 178, 1, 0, 0, 0, 0, 0, 0, 62, 0, 0, 0, 40, 0, 0, 0, 80, 0, 0, 0, 31, 0,
                0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 196, 14, 0, 0, 196, 14, 0, 0, 2, 0,
                0, 0, 2, 0, 0, 0, 0, 0, 0, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
                255, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 255, 255, 255, 255, 255,
                255, 0, 0, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0, 0, 255, 255,
                255, 255, 255, 255, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 255, 255,
                255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0,
                0, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255,
                255, 255, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 255, 255, 255, 255,
                255, 255, 0, 0, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0, 0, 255,
                255, 255, 255, 255, 255, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 255,
                255, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 255, 255, 255, 255, 255,
                255, 0, 0, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0, 0, 255, 255,
                255, 255, 255, 255, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 255, 255,
                255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0,
                0, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255,
                255, 255, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 255, 255, 255, 255,
                255, 255, 0, 0, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0, 0, 255,
                255, 255, 255, 255, 255, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 255,
                255, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 255, 255, 255, 255, 255,
                255, 0, 0, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0, 0, 255, 255,
                255, 255, 255, 255, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 255, 255,
                255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0,
                0, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255,
                255, 255, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 255, 255, 255, 255,
                255, 255, 0, 0
            };
        }

        public PaValidationCodeUtil()
        {
            this.rnd = new Random(unchecked((int)DateTime.Now.Ticks));
        }

        /// <summary>
        /// 生成验证码字符串
        /// </summary>
        /// <returns>验证码字符串</returns>
        public string GernateValidationCode()
        {
            return this.GernateValidationCode(4);
        }

        public string GernateValidationCode(int n)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < n; i++)
            {
                sb.Append(AlphaTable[rnd.Next() % MaxAlphaTable]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获得验证码字符串
        /// </summary>
        /// <returns>验证码字符串</returns>
        public string GetValidationCode()
        {
            return this.ValidationCode;
        }

        /// <summary>
        /// 创建验证码图片的二进制bmp数据
        /// </summary>
        /// <param name="strCode">验证码</param>
        /// <returns>二进制bmp数据</returns>
        public byte[] CreateImageData(string strCode)
        {
            byte[] data = this.CreateEmptyBmpData();

            int nCount = 0;
            if (strCode != null && (nCount = strCode.Length) > 0)
            {
                int offset = System.BitConverter.ToInt32(data, 10);
                int stride = 12;
                int offsetX = 3;

                //////// 最简单实现就是填充对应字节的对应像素，本类型bmp数据流的1字节对应8像素
                ////for (int i = 0; i < nCount; i++)
                ////{
                ////    int n = AlphaTable.IndexOf( strCode[i] );
                ////    if (n < 0 || n >= MaxAlphaTable) continue;
                ////    offsetX = i + 2;
                ////    int offsetY = stride * 20;
                ////    for (int y = 0; y < 16; y++)
                ////    {
                ////        offsetY -= stride;
                ////        data[offset + offsetX + offsetY] = (byte)~charBytes[n, y];
                ////    }
                ////}

                //// 本次实现是用矩阵来映射最终的像素点进行像素填充，绘制的时候变换模式可以多一些，
                //// 旋转 缩放 扭曲 杂色等都可以自有发挥，填充完后再处理成位映射写回 bmp 数据
                byte[,] imageCache = new byte[31, 80];
                int m_nScanLines = 16 - 1;
                ////字符高度一半

                int m_nHalfH = 15;
                ////字符宽度一半
                int m_nHalfW = 8;

                for (int i = 0; i < nCount; i++)
                {
                    int n = AlphaTable.IndexOf(strCode[i]);
                    if (n < 0) continue;

                    double angle = Math.PI / 100 * (rnd.Next(30) - 15);
                    double sin1 = Math.Sin(angle);
                    double cos1 = Math.Cos(angle);

                    /// 最下面一个扫描行总是空白的
                    for (int y = 0; y < m_nScanLines; y++)
                    {
                        byte ch = charBytes[n, y];
                        for (int x = 0; x < 8; x++)
                        {
                            if ((ch & ByteMask[x]) > 0)
                            {
                                //imageCache[y + y, offsetX + x + x] = 1;
                                //imageCache[y + y, offsetX + x + x + 1] = 1;

                                int y1 = m_nHalfH - y * 2;
                                int x1 = m_nHalfW - x * 2;
                                int y2 = y1;
                                int x2 = x1 + 1;
                                int ny1 = m_nHalfH - (int)(cos1 * y1 + sin1 * x1 + 0.5);
                                int nx1 = offsetX + m_nHalfW - (int)(cos1 * x1 - sin1 * y1 + 0.5);
                                int ny2 = m_nHalfH - (int)(cos1 * y2 + sin1 * x2 + 0.5);
                                int nx2 = offsetX + m_nHalfW - (int)(cos1 * x2 - sin1 * y2 + 0.5);
                                if (ny1 >= 0 && ny1 < 31 && nx1 >= 0 && nx1 < 80)
                                    imageCache[ny1, nx1] = 1;
                                if (ny2 >= 0 && ny2 < 31 && nx2 >= 0 && nx2 < 80)
                                    imageCache[ny2, nx2] = 1;
                            }
                            else
                            {
                                imageCache[y + y, offsetX + x + x] = 0;
                            }
                        }
                    }
                    offsetX += 18;
                }

                //// 加一些杂色点
                int randY = 31 - 1, randX = 80 - 1;
                for (int n = 0; n < 100; n++)
                {
                    imageCache[rnd.Next(randY), rnd.Next(randX)] = 1;
                }

                //// 从编辑缓冲区回写到 bmp 字节数据缓冲区
                int offsetL = stride * 31;
                for (int y = 0; y < 31; y++)
                {
                    offsetL -= stride;
                    for (int x = 0; x < 80; x++)
                    {
                        if (imageCache[y, x] > 0)
                        {
                            data[offset + offsetL + x / 8] &= ByteXorMask[x % 8];
                        }
                    }
                }
            }

            ////using (FileStream fs = new FileStream("2bit-cs.bmp", FileMode.Create))
            ////{
            ////    using (BinaryWriter bw = new BinaryWriter(fs))
            ////    {
            ////        bw.Write(data);
            ////    }
            ////}

            return data;
        }

        /// <summary>
        /// 获得 dataUrl 图片内容
        /// </summary>
        /// <param name="strCode">验证码</param>
        /// <returns>内容</returns>
        public string GetDataUrl(string strCode)
        {
            return "data:image/bmp;base64," + Convert.ToBase64String(this.CreateImageData(strCode));
        }

        /// <summary>
        /// 一系列调用方法例子
        /// </summary>
        public void Demo1()
        {
            Console.WriteLine("PaValidationCodeUtil");
            PaValidationCodeUtil vcu = new PaValidationCodeUtil();

            vcu.ValidationCode = vcu.GernateValidationCode();
            string code = vcu.GernateValidationCode();
            //// string code = vcu.ValidationCode;
            string s = vcu.GetDataUrl(code);

            ////byte[] bmpDataEmpty = vcu.CreateEmptyBmpData();
            ////byte[] bmpData = vcu.CreateImageData(code);
            ////using (FileStream fs = new FileStream(String.Format("2bit-cs.bmp", i, n), FileMode.Create))
            ////{
            ////    using (BinaryWriter bw = new BinaryWriter(fs))
            ////    {
            ////        bw.Write(bmpData);
            ////    }
            ////}


            Console.WriteLine(s);
            Console.WriteLine(vcu.ValidationCode);

            /// 初次访问这里会调用一次生成
            Console.WriteLine(vcu.ValidationCode);
            /// 之后访问这里原值并未被修改
            Console.WriteLine(vcu.ValidationCode);
            /// 这也是一种访问方式
            Console.WriteLine(vcu.GetValidationCode());

            ////创建一个80x31大小的空白的 bmp 文件样本
            ////unsafe
            ////{
            ////    //byte[] imageData = new byte[80 / 8 * 31];
            ////    byte[] imageData = new byte[12 * 31];
            ////    for (int i = 0, l = imageData.Length; i < l; i++) {
            ////        imageData[i] = (byte)( i % 12 < 10 ? 255 : 0 );
            ////    }
            ////    int stride = 12;
            ////    fixed (byte* ptr = imageData)
            ////    {
            ////        using (Bitmap image = new Bitmap(80/*width*/, 31/*height*/, stride, PixelFormat.Format1bppIndexed, new IntPtr(ptr)))
            ////        {
            ////            image.Save("2bit.bmp", ImageFormat.Bmp);
            ////        }
            ////    }
            ////}
        }

    }

}
