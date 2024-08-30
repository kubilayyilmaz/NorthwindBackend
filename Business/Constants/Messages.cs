using System;
using Core.Entities.Concrete;

namespace Business.Constants;

public static class Messages
{
    public const string ProductAdded = "Ürün başarıyla kaydedildi.";
    public const string ProductUpdated = "Ürün başarıyla güncellendi.";
    public const string ProductDeleted = "Ürün başarıyla silindi.";

    public const string CategoryAdded =   "Kategori başarıyla kaydedildi.";
    public const string CategoryUpdated = "Kategori başarıyla güncellendi.";
    public const string CategoryDeleted = "Kategori başarıyla silindi.";

    public const string UserAdded =   "Kullanıcı başarıyla kaydedildi.";
    public const string UserNotFound = "Kullanıcı bulunamadı.";
    public const string UserPasswordInCorrect = "Şifre hatalı.";
    public const string UserSuccessfulLogin = "Sisteme giriş başarılı.";
    public const string UserAlreadyExists = "Bu kullanıcı zaten mevcut.";
    public const string UserRegistered = "Kullanıcı başarıyla kaydedildi.";
    public const string UserAccessTokenCreated = "Access token başarıyla oluşturuldu.";
    public const string AuthorizationDenied = "Yetkiniz yok.";

    public const string ProductNameAlreadyExists = "Ürün ismi zaten mevcut.";
}