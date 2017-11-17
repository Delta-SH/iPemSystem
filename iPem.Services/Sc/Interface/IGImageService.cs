using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    /// <summary>
    /// 组态图片API
    /// </summary>
    public partial interface IGImageService {
        /// <summary>
        /// 获得指定名称的图片对象
        /// </summary>
        /// <param name="name">图片名称</param>
        /// <returns>图片对象</returns>
        G_Image GetImage(string name);

        /// <summary>
        /// 判断指定名称的图片对象是否存在
        /// </summary>
        /// <param name="name">图片名称</param>
        /// <returns>true/false</returns>
        Boolean Exist(string name);

        /// <summary>
        /// 获得所有的图片对象集合
        /// </summary>
        /// <returns>图片对象集合</returns>
        List<G_Image> GetImages();

        /// <summary>
        /// 获得指定名称的图片集合
        /// </summary>
        /// <param name="names">需要查询的图片名称集合</param>
        /// <returns>图片集合</returns>
        List<G_Image> GetImages(IList<string> names);

        /// <summary>
        /// 获得所有的原图片对象
        /// </summary>
        /// <returns>图片对象</returns>
        List<G_Image> GetContents();

        /// <summary>
        /// 获得指定名称的原图片对象
        /// </summary>
        /// <returns>图片对象</returns>
        List<G_Image> GetContents(IList<string> names);

        /// <summary>
        /// 获得所有的缩略图对象
        /// </summary>
        /// <returns>缩略图对象</returns>
        List<G_Image> GetThumbnails();

        /// <summary>
        /// 获得指定名称的缩略图对象
        /// </summary>
        /// <param name="names">需要查询的图片名称</param>
        /// <returns>缩略图对象</returns>
        List<G_Image> GetThumbnails(IList<string> names);

        /// <summary>
        /// 获得所有的图片名录
        /// </summary>
        /// <returns>图片名录对象</returns>
        List<G_Image> GetNames();

        /// <summary>
        /// 获得指定名称的图片名录
        /// </summary>
        /// <param name="names">需要查询的图片名称</param>
        /// <returns>图片名录对象</returns>
        List<G_Image> GetNames(IList<string> names);

        /// <summary>
        /// 新增图片
        /// </summary>
        /// <param name="entities">需要新增的图片对象</param>
        void Add(params G_Image[] entities);

        /// <summary>
        /// 更新图片
        /// </summary>
        /// <param name="entities">需要更新的图片对象</param>
        void Update(params G_Image[] entities);

        /// <summary>
        /// 删除指定名称的图片对象
        /// </summary>
        /// <param name="names">需要删除的图片名称</param>
        void Remove(params string[] names);

        /// <summary>
        /// 删除所有的图片对象
        /// </summary>
        void Clear();
    }
}
