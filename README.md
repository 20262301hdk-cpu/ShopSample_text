# CSharpSample

東京ITスクールのASP.NET研修講義資料のサンプルコードをまとめたプロジェクトです。

## 必要環境

- Visual Studio 2022（または互換バージョン）
- SQL Server Developer Edition
- ASP.NET Core MVC、Razor View、Entity Framework Core
- .NET 8.0
- Kestrelサーバ


## 使い方

### 1. リポジトリをクローン

```bash
git clone <リポジトリURL>
cd ShopSample
```

### 2. Visual Studioで開く

ソリューションファイルをダブルクリックして開きます。

```
ShopSample.sln
```

または、Visual Studioを起動してから「ファイル」→「開く」→「プロジェクト/ソリューション」から選択します。

### 3. データベースの更新

マイグレーションをデータベースに適用します。

パッケージマネージャーコンソールでの実行

```powershell
Update-Database
```

### 4. ビルドと実行

- ツールバーの「開始」ボタン（緑の再生アイコン）をクリック
- または`F5`キーを押して実行

## プロジェクト内容

ASP.NET Core MVCアプリケーションで、C# .NETによるWebアプリケーション構築を学習できる各種サンプルコードを含んでいます。

### 章ごとのソースコード

講義資料の章ごとにフォルダが分けてあり、その章で作成する（または修正する）ファイルのみをコピーしてあります。
昇順に、空のプロジェクトにファイルをコピーしていくと、最終的に完成版プロジェクトが完成する、ということです。
ShopSampleフォルダ配下の階層構造をそのままにコピーしているため、ShopSampleフォルダ直下にコピペすればOKです。