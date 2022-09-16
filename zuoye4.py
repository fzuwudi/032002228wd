import requests
import lxml.etree
from lxml import etree
import pandas as pd
import re
import os
cookies = {
    'yfx_c_g_u_id_10006654': '_ck22091015111912117933959175841',
    'sVoELocvxVW0S': '5ZK7.umO85UOS6zmF7f8md6WWcIlDbT6ABKG29Q1nqkx0I92Ak0eVZtLQEgE0Y3imld.YAiUQi4xzxEFdDKanwG',
    'insert_cookie': '96816998',
    'yfx_f_l_v_t_10006654': 'f_t_1662793879214__r_t_1663048365060__v_t_1663068814254__r_c_3',
    'security_session_verify': '0b3e5e432a0cd0cca9d59a9c0ae9ec6d',
    'sVoELocvxVW0T': '53nqG8DWGEo9qqqDkJj1lgG9Vhz8AKxRhwfboIjQ093hXUPPP31oFVACzWoveONjxXxf.Tg88Gs9zLvp8eRBvrHcnB23_SW8buKfvfAEYj541E3PQq0dqKIQzeSWUgir0YOE0Tl84wKjH4FtbTmDtQhOs.elRTFNm6vgmwElpTFe76ctNeL9gcVqP5Tv7pUioboR5BgrXWgFCB6C4eS3TN62IIS.Z7mzgGhek3HPpZ40iRf7jyySzZNdkCd27xTPyQdG3BMxBGc1Gqng0KJV_UL_45mkK_bDdr5pYJLQWNdozuxSgAGeS8QNjXXw93EXEd.FZh3.P6odHE0IRamdqwsf9lVUP4vDONsszqeBr5o2G',
}
headers = {
    'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9',
    'Accept-Language': 'zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6',
    'Cache-Control': 'max-age=0',
    'Connection': 'keep-alive',
    'Upgrade-Insecure-Requests': '1',
    'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36 Edg/105.0.1343.33',
}
wanzhi=[]
with open('web.txt','r',encoding='utf-8') as f:
    shuju=f.readlines()
    #按行读取
    #第一步是以字符串的形式存入的，所以现在按行读取出来的数据是一个又一个的字符串，每个字符串后面都有一个'\n'
    for i in shuju:
        i=i.replace('\n','')
        #把'\n'用''代替
        i=i.split()
        #以空格为分隔符
        i[0]='http://www.nhc.gov.cn'+i[0]
        #刚刚爬取得到的路径是不完整的，需要加上头部
        wanzhi.append(i)
# print(wanzhi)
for i in wanzhi[1:254]:
    #2022.1.1    254
    url=i[0]
    shiqi=i[1]
    page_text=requests .get(url=url,headers=headers,cookies=cookies, verify=False).text
    tree= etree.HTML(page_text)
    #数据解析（实例化）
    fp=open ('2022'+shiqi+'.txt','a',encoding='utf-8')
    sentence=tree.xpath('//div[@class="con"]//text()')
    for sen in sentence:
        fp.write(sen)
for i in wanzhi[254:619]:
    #2022.1.1    254
    url=i[0]
    shiqi=i[1]
    page_text=requests .get(url=url,headers=headers,cookies=cookies, verify=False).text
    tree= etree.HTML(page_text)
    #数据解析（实例化）
    fp=open ('2021'+shiqi+'.txt','a',encoding='utf-8')
    sentence=tree.xpath('//div[@class="con"]//text()')
    for sen in sentence:
        fp.write(sen)
for i in wanzhi[619:]:
    #2022.1.1    254
    url=i[0]
    shiqi=i[1]
    page_text=requests .get(url=url,headers=headers,cookies=cookies, verify=False).text
    tree= etree.HTML(page_text)
    #数据解析（实例化）
    fp=open ('2020'+shiqi+'.txt','a',encoding='utf-8')
    sentence=tree.xpath('//div[@class="con"]//text()')
    for sen in sentence:
        fp.write(sen)
for i in wanzhi[1:]:
    shiqi=i[1]
    print(shiqi)
    f=open('2021'+shiqi+'.txt',encoding='utf-8')   #设置文件对象
    str=f.read()     #将txt文件的所有内容读入到字符串str中
    f.close()   #将文件关闭
    # print(str)
    全国新增确诊=re.findall('新增确诊病例.*?本土病例(.*?)例', str)[0]
    # print(全国新增确诊)
    全国新增无症状=re.findall('新增无症状感染者.*?本土(.*?)例', str)[0]
    # print(全国新增无症状)
    全省新增确诊=re.findall('新增确诊病例.*?本土病例.*?例（(.*?)），', str)[0]
    # print(全省新增确诊)
    全省新增无症状=re.findall('新增无症状感染者.*?本土.*?例（(.*?)）。', str)[0]
    # print(全省新增无症状)
    #全国新增确诊、全国新增无症状、全省新增确诊、全省新增无症状都是str
    全省新增确诊= 全省新增确诊.split('，')
    全省新增确诊 = [i.replace('例', '') for i in 全省新增确诊]
    全省新增确诊数据=[]
    for i in 全省新增确诊:
        for j in i:
            if '0' <= j <= '9':
                汉字 = i[:i.index(j)]
                数字 = i[i.index(j):]
                全省新增确诊数据.append([汉字, 数字])
                break;
    # print(全省新增确诊数据)
    全省新增无症状= 全省新增无症状.split('，')
    全省新增无症状 = [i.replace('例', '') for i in 全省新增无症状]
    全省新增无症状数据=[]
    for i in 全省新增无症状:
        for j in i:
            if '0' <= j <= '9':
                汉字 = i[:i.index(j)]
                数字 = i[i.index(j):]
                全省新增无症状数据.append([汉字, 数字])
                break;
    # print(全省新增无症状数据)
    省 = '山西省，辽宁省，吉林省，黑龙江省，江苏省，浙江省，安徽省，福建省，江西省，山东省，河南省，湖北省，湖南省，广东省，海南省，四川省，贵州省，云南省，陕西省，甘肃省，青海省，北京，天津，上海，重庆，内蒙古，广西，宁夏，新疆，西藏，河北'
    省 = [i.replace('省', '') for i in 省.split('，')]
    moban=[0]*62
    for l in 全省新增确诊数据:
        try:
            moban[省.index(l[0])] = l[1]
        except:
            continue
    for l in 全省新增无症状数据:
        try:
            moban[省.index(l[0]) + 31] = l[1]
        except:
            continue
    省 = [i + '确诊' for i in 省] + [i + '无症状' for i in 省]
    moban.append(全国新增确诊)
    moban.append(全国新增无症状)
    moban.append(shiqi)
    省.append('全国确诊')
    省.append('全国无症状')
    省.append('日期')
    print(省)
    print(moban)
    zid={}
    for ki,kd in zip(省,moban):
        zid[ki]=[kd]

    if os.path.exists('shujuji3.xlsx'):
        df=pd.read_excel('shujuji3.xlsx')
        df=df.append(pd.DataFrame(zid))
        df.to_excel('shujuji3.xlsx',index=None)
    else:
        pd.DataFrame(zid).to_excel('shujuji3.xlsx',index=None)