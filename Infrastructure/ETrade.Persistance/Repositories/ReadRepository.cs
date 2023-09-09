using ETrade.Application.Repositories;
using ETrade.Domain.Common;
using ETrade.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Persistance.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ETradeAPIDBContext _context;

        public ReadRepository(ETradeAPIDBContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }
        public async Task<T> GetById(int id, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = Table.AsNoTracking();

            }
            return await query.FirstOrDefaultAsync(data => data.Id == id);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, bool tracking = true)
        { 
            var query=Table.AsQueryable();
            if (!tracking)
            {
                query = Table.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(filter);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> filter, bool tracking = true)
        {
            var query = Table.Where(filter);
            if (!tracking)
            {
                query.AsNoTracking();
            }
            return query;
        }
    }
}
/*
 Öncelikle Read ve Write işlemlerini solid prensipleri gereği ayırdık. Ancak bu iki interface'in açık kalmaması için Bir ana İnterface'i implemente ettirdik. Bu iki interface için de ortak olan tek şey Tablonun kendisidir. Bu tablo işlemleri yapmak için işimize yarayacaktır. Bu tablonun içine DbSet<Table> kullanarak get ile getirme izması atuyoruz. Bunu yazmamızın sebebi hem read hem de write işlemlerinde tabloya olan ihtiyacımızdan kaynaklanmaktadır. Read ve Write işlemleri için de interfaceler oluşturuyoruz. Bütün bunları generic yapıda oluşturacağımız için bir T parametresi almalıdır ve bu T parametresinin bir class olması lazım. Bu classı Id'ye göre getirme işleminde FirtOrDefault işleminde Id'nin görünebilmesi için class ile değil de Entitiler için oluşturduğumuz Base sınıftan çekmeliyiz. Çünkü bu base sınıfı bütün entityler için ortak değer olan ID propertysini yapısında bulundururyor. Öbür türlü lambda ile id değerlerini eşleştiremiyoruz. Yani tekrar söylüyorum T'nin bir Class değil de BaseEntity'den miras almaktadır. Sonuç olarak baktığımızda BaseEntity de bir classtır. 

    Daha sonra IReadRepository için imzalarımızı yazarız. Burada listeleme işlemlerini yaparız.
    Ancak listeleme işlemi list ile değil IQueryable ile yapılır. İşlemlerimiz; tümünü listeleme, Id'ye göre listeleme, şartlara göre listelemedir. Bu tabi ki arttırılabilir. Ancak genel olarak bu işlemler kullanılır. Tek bir T değeri döndürürken de Task<T> ile döndürme işlemi yaparız. Daha sonra IWriteRepository için imzalarımızı atmamız gerekir. Burada ekleme, güncelleme, silme, ID'ye göre silme gibi işlemler gerçekleştirilir. Bu işlemlerin imzası atılırken başına yine task eklenir. Ancak bu sefer task<bool> Add() gibi bir syntax kullanılır. 

    Gelelim Read interface'i için bir concrete dosyası oluşturmaya. Bunu oluşturacağımız yer Persistance katmanı içindedir. Çünkü mimariye göre application katmanında interface i oluşturulurken, Persistance katmanında da Concrete'i oluşturulur. ReadRepository IReadRepository'i implemente eder. IReadRepository bir T türünde parametre aldığı için ReadRepository de bir T türünde parametre alır. Yine T'nin bir BaseEntity olduğunu yani bir class olduğunu belirtiriz. 
    
    Constructor ile context nesnemizi oluşturup, tablolarımızı çekeriz. 


 



Öcnelikle veritabanına bağlandıktan sonra imza metodularının ardından using ile context kullanmak yerine  dependency injection yaparak ve constructor oluşturarak yaparız. Read ve Write işlemlerini ayırdıktan sonra 
 
 
 
 
 */