﻿using Ecommerce.DAL.models;
using Ecommerce.DAL.Repository;
using System.Security.Cryptography;

namespace Ecommerce.BL.Services
{
    public class Applieddiscount : Iapplydiscountservice
    {
        public IRepository<discountapplied_product> repository;
        public IRepository<discount> discount_repository;
        public Iproductservice productservice;
        public Applieddiscount( Iproductservice productservice, IRepository<discountapplied_product> repository, IRepository<discount> discount_repository) 
        {
         
            this.productservice = productservice;
            this.repository = repository;
            this.discount_repository = discount_repository;
        }

        public async Task Addproductdiscount(discountapplied_product dsap) 
        {
           var discount = await discount_repository.Getbyidasync(dsap.discount_id);

            var product =  await productservice.GetProductaByIdAsync(dsap.product_id);

            if(product== null || discount == null) 
            {
                return ;
            }

            await repository.Add(dsap);
        }

        public async Task<IList<discountapplied_product>> listproductdiscount() 
        {
            var applylist = await repository.GetAllasync();

            return applylist;
        }

        public async Task<discountapplied_product> getproductdiscount(int id) 
        {
            var applydiscount = await repository.Getbyidasync(id);
            return applydiscount;
        }


        public async Task applyproductdiscount(int id) 
        {
            var applydiscount =  await getproductdiscount(id);

            var product = await productservice.GetProductaByIdAsync(applydiscount.product_id);
            var discount = await discount_repository.Getbyidasync(applydiscount.discount_id);

            if (discount.discount_startdate > DateTime.Now || discount.discount_startdate == DateTime.Now) 
            {
                product.old_price = product.price;
                product.price = product.price - (product.price * discount.discount_percentage);

             await  productservice.Updateproduct(product);
            }

            return;
        }

        public void updateproductdiscount(discountapplied_product dsap) 
        {
            repository.Update(dsap);
        }

        public async Task deleteproductdiscount(discountapplied_product discountapplied_Product) 
        {
             
            

            var product = await productservice.GetProductaByIdAsync(discountapplied_Product.product_id);
            var discount = await discount_repository.Getbyidasync(discountapplied_Product.discount_id);

            product.price = product.old_price;
            product.old_price = product.price;
           

           await productservice.Updateproduct(product);
            await repository.Delete(discountapplied_Product.ID);
        }


       
    }
}
