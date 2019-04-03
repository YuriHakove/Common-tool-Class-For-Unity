using UnityEngine;
using System.Collections;
using System.Collections.Generic;
///
//����ʹ��ʾ��
// Sprite _sprite = SpritesManager.getInstance().LoadAtlasSprite("common/game/CommPackAltas","Сͼ����");
///
//����ͼ�����ع���
public class SpritesManager : MonoBehaviour {
	private static GameObject m_pMainObject;
	private static PPTextureManage m_pContainer = null;
	public static PPTextureManage getInstance(){
		if(m_pContainer == null){
			m_pContainer = m_pMainObject.GetComponent<PPTextureManage> ();
		}
		return m_pContainer;
	}
	private Dictionary<string, Object[]> m_pAtlasDic;//ͼ���ļ���
	void Awake(){
		initData ();
	}
	private void initData(){
		PPTextureManage.m_pMainObject = gameObject;
		m_pAtlasDic = new Dictionary<string, Object[]> ();
	}
	// Use this for initialization
	void Start () {
	}
	//����ͼ���ϵ�һ������
	public Sprite LoadAtlasSprite(string _spriteAtlasPath,string _spriteName){
		Sprite _sprite = FindSpriteFormBuffer (_spriteAtlasPath,_spriteName);
		if (_sprite == null) {
			Object[] _atlas = Resources.LoadAll (_spriteAtlasPath);
			m_pAtlasDic.Add (_spriteAtlasPath,_atlas);
			_sprite = SpriteFormAtlas (_atlas,_spriteName);
		}
		return _sprite;
	}
	//ɾ��ͼ������
	public void DeleteAtlas(string _spriteAtlasPath){
		if (m_pAtlasDic.ContainsKey (_spriteAtlasPath)) {
			m_pAtlasDic.Remove (_spriteAtlasPath);
		}
	}
	//�ӻ����в���ͼ�������ҳ�sprite
	private Sprite FindSpriteFormBuffer(string _spriteAtlasPath,string _spriteName){
		if (m_pAtlasDic.ContainsKey (_spriteAtlasPath)) {
			Object[] _atlas = m_pAtlasDic[_spriteAtlasPath];
			Sprite _sprite = SpriteFormAtlas(_atlas,_spriteName);
			return _sprite;
		}
		return null;
	}
	//��ͼ���У����ҳ�sprite
	private Sprite SpriteFormAtlas(Object[] _atlas,string _spriteName){
		for (int i = 0; i < _atlas.Length; i++) {
			if (_atlas [i].GetType () == typeof(UnityEngine.Sprite)) {
				if(_atlas [i].name == _spriteName){
					return (Sprite)_atlas [i];
				}
			}
		}
		Debug.LogWarning ("ͼƬ��:"+_spriteName+";��ͼ�����Ҳ���");
		return null;
	}
}