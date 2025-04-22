using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Session2SApp.Droid
{
    public class OrderFragment : Fragment
    {
        private RecyclerView productRecyclerView;
        private LinearLayout categoryContent;
        private TextView cartCountText;
        private TextView cartTotalText;

        private List<Product> products = new List<Product>();
        private List<Category> categories = new List<Category>();
        private Dictionary<int, int> cartItems = new Dictionary<int, int>();
        private int currentCategoryId = 0;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var view = inflater.Inflate(Resource.Layout.order_fragment, container, false);
            productRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.product_list);
            categoryContent = view.FindViewById<LinearLayout>(Resource.Id.category_content);
            //设置商品为列表布局
            var layoutManager = new LinearLayoutManager(Context);
            productRecyclerView.SetLayoutManager(layoutManager);
            cartCountText = view.FindViewById<TextView>(Resource.Id.cart_count);
            cartTotalText = view.FindViewById<TextView>(Resource.Id.cart_total);
            LoadTestData();
            InitCategories();
            UpdateProductList(currentCategoryId);
            return view;
        }

        private void LoadTestData()
        {
            // 添加测试分类
            categories.Add(new Category(1, "全部"));
            categories.Add(new Category(2, "热销"));
            categories.Add(new Category(3, "套餐"));
            categories.Add(new Category(4, "小吃"));
            categories.Add(new Category(5, "饮料"));

            // 添加测试商品
            products.Add(new Product(1, "汉堡", 15, 2));
            products.Add(new Product(2, "薯条", 8, 2));
            products.Add(new Product(3, "可乐", 6, 5));
            products.Add(new Product(4, "鸡翅", 10, 4));
            products.Add(new Product(5, "冰淇淋", 5, 5));
        }

        private void InitCategories()
        {
            categoryContent.RemoveAllViews();
            foreach(var c in categories)
            {
                var cView = LayoutInflater.From(Context).Inflate(Resource.Layout.category_item, categoryContent, false);
                var cViewText = cView.FindViewById<TextView>(Resource.Id.category_name);
                cViewText.Text = c.Name;
                cView.Click += (sender, e) =>
                {
                    currentCategoryId = c.Id;
                    UpdateProductList(currentCategoryId);
                    UpdateCategorySelection(c.Id);
                };
                categoryContent.AddView(cView);
            }
        }

        private void UpdateProductList(int categoryId)
        {
            var filteredProducts = categoryId == 1
                ? products
                : products.Where(p => p.CategoryId == categoryId).ToList();

            var adapter = new ProductAdapter(filteredProducts, AddToCart);
            productRecyclerView.SetAdapter(adapter);
        }

        private void UpdateCategorySelection(int categoryId)
        {
            for (int i = 0; i < categoryContent.ChildCount; i++)
            {
                var child = categoryContent.GetChildAt(i);
                var textView = child.FindViewById<TextView>(Resource.Id.category_name);
                textView.SetTextColor(categoryId == categories[i].Id
                    ? Color.ParseColor("#FF4081")
                    : Color.ParseColor("#333333"));
            }
        }

        private void AddToCart(Product product)
        {
            if (cartItems.ContainsKey(product.Id))
            {
                cartItems[product.Id]++;
            }
            else
            {
                cartItems.Add(product.Id, 1);
            }

            UpdateCartInfo();
        }

        private void UpdateCartInfo()
        {
            int totalCount = cartItems.Sum(item => item.Value);
            decimal totalPrice = cartItems.Sum(item => {
                var product = products.First(p => p.Id == item.Key);
                return product.Price * item.Value;
            });

            cartCountText.Text = totalCount.ToString();
            cartTotalText.Text = $"¥{totalPrice}";
        }
    }

    // 商品适配器
    public class ProductAdapter : RecyclerView.Adapter
    {
        private List<Product> products;
        private Action<Product> addToCartAction;

        public ProductAdapter(List<Product> products, Action<Product> addToCartAction)
        {
            this.products = products;
            this.addToCartAction = addToCartAction;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.product_item, parent, false);
            return new ProductViewHolder(itemView);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var product = products[position];
            var viewHolder = holder as ProductViewHolder;

            viewHolder.NameText.Text = product.Name;
            viewHolder.PriceText.Text = $"¥{product.Price}";
            viewHolder.AddButton.Click += (sender, e) => addToCartAction(product);
        }

        public override int ItemCount => products.Count;
    }

    // 商品ViewHolder
    public class ProductViewHolder : RecyclerView.ViewHolder
    {
        public TextView NameText { get; }
        public TextView PriceText { get; }
        public Button AddButton { get; }

        public ProductViewHolder(View itemView) : base(itemView)
        {
            NameText = itemView.FindViewById<TextView>(Resource.Id.product_name);
            PriceText = itemView.FindViewById<TextView>(Resource.Id.product_price);
            AddButton = itemView.FindViewById<Button>(Resource.Id.add_to_cart);
        }
    }

    // 数据模型
    public class Product
    {
        public int Id { get; }
        public string Name { get; }
        public decimal Price { get; }
        public int CategoryId { get; }

        public Product(int id, string name, decimal price, int categoryId)
        {
            Id = id;
            Name = name;
            Price = price;
            CategoryId = categoryId;
        }
    }

    public class Category
    {
        public int Id { get; }
        public string Name { get; }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}