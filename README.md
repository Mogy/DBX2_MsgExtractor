# DBX2_MsgExtractor

Dragon Ball Xenoverse 2 MSG Tool By Delutto を使ってメッセージデータの一覧を抽出するための補助ツール

ゲームに使用するメッセージデータを作成したい場合は[こちら](https://github.com/Mogy/DBX2_MsgCreator)

実行には[.Net Core Runtime 3.1](https://www.ipentec.com/document/windows-install-dotnet-core-31-runtime)が必要

変換元のメッセージデータは各自で吸い出して用意する

# 必要なもの

* [Dragon Ball Xenoverse 2 MSG Tool By Delutto](https://zenhax.com/viewtopic.php?t=4052#p35491)
* CS版の英語メッセージデータ(*_en.msg)
* CS版の日本語メッセージデータ(*_ja.msg)

# 作業手順

1. Dragon Ball Xenoverse 2 MSG Tool By Delutto をダウンロードして実行ファイルを同ディレクトリに置く
2. CS版の英語メッセージデータ(*_en.msg)を「CsEnMsgフォルダ」に追加する
3. CS版の日本語メッセージデータ(*_ja.msg)を「CsJaMsgフォルダ」に追加する
4. DBX2_MsgCreator.exe を実行
5. 抽出したメッセージの一覧が「enMsg.txt」「jaMsg.txt」にそれぞれ出力される
