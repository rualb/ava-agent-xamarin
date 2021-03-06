using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.Common.Const
{
    public enum ConstMaterialType : int
    {
        undef = -1,
        commercialGood = 1, // Ticari Mal, Commercial Good,Коммерческий Товар
        mixedCase = 2, // Karma Koli,  Mixed Case,Смешанная Упаковка
        depositedItem = 3, // Depozitolu Mal, Deposited Item,Депонированный Товар
        fixedAsset = 4, // Sabit Kıymet, Fixed Asset,Основные Средства
        rawMaterial = 10, // Hammadde, Raw Material,Сырье
        semiFinishedGood = 11, // Yarı Mamul, Semi Finished Good,Полуфабрикат
        finishedGood = 12, // Mamul, Finished Good,Продукция
        consumerGoods = 13, // Tüketim Malı, Consumer Goods,Расходные Материалы
        materialClassG = 20, // Genel Malzeme Sınıfı, Material Class (General),Общий Класс Материала
        materialClassT = 21 // Tablolu Malzeme Sınıfı) Material Class (With table),Табличный Класс Материала




    }
}


