import requests
def pa(yeshu):
    #用于爬取目录，获得每一页所对应的源码
    cookies = {
        'yfx_c_g_u_id_10006654': '_ck22091015111912117933959175841',
        'sVoELocvxVW0S': '5ZK7.umO85UOS6zmF7f8md6WWcIlDbT6ABKG29Q1nqkx0I92Ak0eVZtLQEgE0Y3imld.YAiUQi4xzxEFdDKanwG',
        'insert_cookie': '96816998',
        'yfx_f_l_v_t_10006654': 'f_t_1662793879214__r_t_1662952265663__v_t_1662961949729__r_c_2',
        'security_session_verify': 'b4a8aedafcebc0a0e015ad2282411b67',
        'sVoELocvxVW0T': '53S4tSDW8n7VqqqDkWmb1SqeqwxmDf2luFErZgLoJO2533rLYtd0Swz583y.XjBpFEEylnUI2NsmakZc_R_yxT6dA_pPFkBvjm778sspA5h8ubo_tHh1EigI4iLxMxF_01lY5_w_eG6.5VdWerk8ef370buSmLkD7gYp.J_QQN5wbNQbkF6rNvc2gklIvkMXBm_wRzBWqiJusa7tu52ABpt5kn1VVgN_m6CRuANXpqB0ez0vm25csbQh2g1feeHjouJpD37R5H7QAmHxKOjEG8acx3POO8chDeWuzNa01nmX3ZktyMzPvwQYxI0Lavdf15KJ8BEEnxUCriQl8qXRKp3Nq_I__lIjyo6BB5TRpq3WA',
    }
    #有反爬机制，需要使用cookie。cookie也有使用时间限制，时间到了就需要更换。
    headers = {
        'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9',
        'Accept-Language': 'zh-CN,zh;q=0.9',
        'Connection': 'keep-alive',
        'Referer': 'http://www.nhc.gov.cn/xcs/yqtb/list_gzbd.shtml',
        'Upgrade-Insecure-Requests': '1',
        'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36',
    }
    if yeshu > 1:
        response = requests.get(f'http://www.nhc.gov.cn/xcs/yqtb/list_gzbd_{yeshu}.shtml', headers=headers, cookies=cookies, verify=False)
    else:
        response = requests.get('http://www.nhc.gov.cn/xcs/yqtb/list_gzbd.shtml', headers=headers, cookies=cookies, verify=False)
    #除第一页外每一页的网址都遵顼规律，故对第一页的网址特殊化处理。
    return response
    #返回每一页的源码
import re
for i in range(1,42):  #更改数字 代表  页数
    response=pa(i)
    response=response.text
    网址=re.findall('<a href="(.*?)" t.*?t.*?截至(.*?)24时',response)
    #一开始写的的是'<a href="(.*?)" t.t.截至(.*?)2'，显然是错了，因为混淆了'.'和'.*?'的作用
    #第二次写的是'<a href="(.*?)" t.*?t.*?截至(.*?)2'，想偷懒不写'24小时'，但事实是，这样子出来的结果日期会出问题，因为日期也有2开头的。
    #使用正则表达式
    print(网址)
    with open('web.txt','a',encoding='utf-8') as f:
    #写入txt文件，使用'a',代表追加
        for i in 网址:
            f.write(i[0]+'    '+i[1]+'\n')
            #用空格把具体网址和日期分开