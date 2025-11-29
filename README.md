<H1>Pattern Learn Dungeon</H1>

[![Demo Gameplay](https://img.youtube.com/vi/https://youtu.be/ENIxU4cSGOk/maxresdefault.jpg)](https://youtu.be/ENIxU4cSGOk)




<H3>1. Singleton Pattern</H3>

<H4>class GameManager</H4>
<img width="558" height="338" alt="image" src="https://github.com/user-attachments/assets/cafce6d9-76ef-477f-845c-3d483d203b2e" />
<h4>class EventManager</h4>
<img width="868" height="314" alt="image" src="https://github.com/user-attachments/assets/e8f1727e-c5bb-4029-814d-4363d77574be" />
<br>
<br>
  เป็นรูปแบบ หรือ pattern ที่ทำให้ class มี instance เพียงตัวเดียวในเกมและสามารถเข้าถึงได้จากทุกที่ 
ประโยชน์: ทำให้สามารถเข้าถึงระบบหลักๆ ได้ง่ายโดยไม่ต้อง reference ใน Inspector อีกทั้งทำให้เรียกใช้งานและเข้าถึงข้อมูลได้ผ่านการเรียกใน code 
ตัวอย่างในโปรเจกต์:
GameManager: ใช้ public static GameManager instance; 
ทำให้คลาสอื่นๆ เช่น UIManager หรือ TurnBaseSystem สามารถเรียกใช้ข้อมูลส่วนกลางได้ทันทีผ่าน GameManager.instance
EventManager: เป็นศูนย์กลางการที่ทุก script ต่างๆสามารถเข้าถึงได้เพื่อส่งหรือรับ event

<hr>

<H3>2. Facade Pattern </H3>

<h4>References in GameManager</h4>
<img width="503" height="737" alt="image" src="https://github.com/user-attachments/assets/402abc9b-7b4d-4c11-9db0-57a2dbde7a39" />
<h4>References in GameManager</h4>
<img width="805" height="537" alt="image" src="https://github.com/user-attachments/assets/0af954e3-e971-48ab-b047-174d4e678dfb" />
<br>
<br>
เป็นการใช้ class กลางเพื่อรวบรวมและลดความซับซ้อนของระบบย่อยต่างๆ โดยการรวมไว้ด้วยกันที่ class กลาง 
ประโยชน์: ทำให้โค้ดส่วนอื่นไม่ต้องรู้ว่าระบบย่อยทำงานยังไง แค่สั่งงานผ่านตัวกลางก็พอ ช่วยลดปัญหา spaghetti code ได้ 
ตัวอย่างในโปรเจกต์:
GameManager: ทำหน้าที่เป็น Facade ที่ทำการ reference TurnBaseSystem, CharacterBuilder, และ UIManager ดังนั้นเมื่อ GameState ต้องการเริ่มเกม มันแค่สั่งผ่าน GameManager ไม่ต้องไปคุยกับระบบย่อยทีละตัว
TurnBaseSystem: ทำหน้าที่เป็น Facade ของระบบต่อสู้ คอยจัดการทั้ง EnemyFactory, การคำนวณ Damage, และการเปลี่ยน Phase ของเกม

<hr>

<H3>3. State Pattern </H3> 

<h4>abstract class GameState</h4>
<img width="386" height="230" alt="image" src="https://github.com/user-attachments/assets/572f1938-40eb-4e58-875b-145818b2823d" />
<h4>Example use in StartState</h4>
<img width="725" height="697" alt="image" src="https://github.com/user-attachments/assets/f3c4e6c4-7f3e-414c-9615-8aebac39e1a2" />

<br>
<br>
  จะเป็นการแบ่งการทำงานของเกมออกเป็น สถานะ หรือ state ย่อยๆ โดยแต่ละสถานะจะมี logic ในการทำงานของตัวเองแยกออกจากกัน 
ประโยชน์: แก้ปัญหาการใช้ if-else จำนวนมากใน Update() ทำให้การจัดการสิ่งที่เกิดขึ้นในเกมทำได้ง่ายขึ้นและเป็นระเบียบและแก้ไขง่าย อีกทั้งยังช่วยลดปัญหาการต้องตรวจสอบเงื่อนไขตลอดเวลาขณะเกมเล่นอยู่ด้วย
ตัวอย่างในโปรเจกต์:
Class GameState ทำหน้าที่เป็นแม่แบบ แล้วมีลูกคือ StartState, CharacterSelectState, GameplayState ซึ่ง inherit จาก GameState อีกทีหนึ่งทำให้เวลาสั่ง ChangeState(new GameplayState(...)) เกมก็จะสลับ Logic ไปยัง state ใหม่ที่ทำการสั่ง เช่น จากหน้าเลือกตัวละคร สลับไปยังหน้าต่อสู้แทน โดยทำการเปิด UI เกมเพลย์ และปิด UI หน้าเลือกตัวละคร

<hr>


<H3>4. Builder Pattern </H3>

<h4>class CharacterBuilder</h4>
<img width="582" height="768" alt="image" src="https://github.com/user-attachments/assets/a3245ca6-ef52-49d1-a191-f6ed159d87ef" />
<h4>class CharacterConfig as ScriptableObject</h4>
<img width="902" height="333" alt="image" src="https://github.com/user-attachments/assets/5a03dbec-b143-4e43-b6cb-d5aa56353770" />

<br>
<br>
  เป็นรูปแบบการสร้าง object โดยการค่อยๆ ประกอบชิ้นส่วนทีละขั้นตอน แทนการโยนพารามิเตอร์ทั้งหมดเข้าไปใน Constructor เดียว 
ประโยชน์: ทำให้อ่านโค้ดง่าย ปรับเปลี่ยนค่า config ได้สะดวก และแยก Logic การสร้างออกจากตัว object นั้นๆ 
ตัวอย่างในโปรเจกต์:
CharacterBuilder: แทนที่จะเขียน constructor ของ CharacterBuilder ยาวๆ เป็น new Character(hp, ap, weapon, ...) ก็จะใช้ StartNewCharacter().ApplyConfig(config).Build() แทน ในการเรียกใช้การ build นั้นใน ApplyConfig จะทำการแปลงค่าจาก ScriptableObject (CharacterConfig) ที่ได้ทำการ setting config ของตัวละครต่างๆเอาไว้มาเป็นค่าสถานะต่างๆของตัวละคร เช่น เลือด ชื่อ พลังโจมตี รวมถึงการเลือกใส่อาวุธ (Weapon) ให้ตรงกับอาชีพตาม config


<hr>


<H3>5. Factory Pattern </H3>

<h4>class EnemyFactory, CreateEnemy method</h4>
<img width="622" height="516" alt="image" src="https://github.com/user-attachments/assets/4734ba43-18c4-4b52-a886-5d1804ebeb8e" />
<h4>class EnemyFactory, BuildEnemy method</h4>
<img width="601" height="682" alt="image" src="https://github.com/user-attachments/assets/d36a8f2f-993f-4df6-bf83-8907a7c59c37" />
<h4>class EnemyConfig as ScriptableObject</h4>
<img width="615" height="233" alt="image" src="https://github.com/user-attachments/assets/260c0629-3968-42c0-a9f0-c0680aa131cc" />

<br>
<br>
  เป็นการสร้าง object โดยให้ class ย่อยหรือ method เป็นตัวตัดสินใจว่าจะสร้าง object ชนิดไหน แทนที่จะระบุ class เจาะจงลงไปตรงๆ 
ประโยชน์: ทำให้เกิดความยืดหยุ่นต่อการเพิ่มชนิดของ object ใหม่ๆเพราะไม่ได้เจาะจงและไม่ได้รู้จักกันโดยตรง 
ตัวอย่างในโปรเจกต์:
EnemyFactory: จะมี method CreateEnemy(roomNumber) ซึ่จะใช้ตัดสินใจว่าถ้า room % 10 == 0 ให้สร้าง Boss หรือหมายถึงว่าถ้าเป็นห้องที่ลงด้วยทุกๆ 10 จะให้ทำการสร้าง Boss monster แต่ถ้าไม่ใช่ ให้สุ่มสร้างมอนสเตอร์ทั่วไป
ในส่วนของ TurnBaseSystem โดยไม่ต้องรู้ว่าห้องนั้นจะต้องมี monster หรือ enemy ตัวไหน แค่สั่งกับ EnemyFactory โดยเรียก CreateEnemy(roomNumber) EnemyFactory ก็จะจัดการสร้างและส่งคืนมาให้ แล้วสามารถนำมาใช้งานได้ต่อไป


<hr>


<H3>6. Command Pattern</H3>

<h4>interface ICommand</h4>
<img width="429" height="299" alt="image" src="https://github.com/user-attachments/assets/214c1cff-eecf-4fda-aaac-31eebcd257e4" />
<h4>Example AttackCommand</h4>
<img width="678" height="806" alt="image" src="https://github.com/user-attachments/assets/c05c2abd-56ce-40a6-9e00-1a251392742a" />
<h4>Example Defense Command</h4>
<img width="705" height="796" alt="image" src="https://github.com/user-attachments/assets/77067d83-bc8e-410b-9907-c59b644f36b9" />

<br>
<br>
  จะเป็นการทำให้ action อยู่ในรูปของ object ทำให้สามารถเก็บข้อมูลต่างๆสำหรับการทำ action นั้นได้ 
ประโยชน์: ทำให้เราสามารถเก็บประวัติการกระทำเป็น history ในรูปของ stack หรือ queqe ก็ได้ เพื่อทำระบบ Undo และแยกตัวสั่งการออกจากตัวทำงาน
ตัวอย่างในโปรเจกต์:
ICommand: เป็น Interface ที่มี Execute() และ Undo()
AttackCommand, DefenseCommand: จะ inherit มาจาก ICommand ทำให้มี Execute() และ Undo() ด้วย โดยเมื่อผู้เล่นกดโจมตี หรือมอนสเตอร์โจมตี จะทำการสร้าง new AttackCommand(source, target) แล้วส่งข้อมูลนี้ต่อ ซึ่งส่วนที่รับไปก็จะค่อยไปจัดการเรื่องการทำความเสียหายว่าใครโจมตีใครอีกที โดยไม่ต้องรู้จักส่วนที่ทำการสั่งโจมตี ซึ่งเมื่อทำการ Execute() แต่ละครั้งก็จะทำการ stack เก็บคำสั่งที่ทำไว้ ทำให้สามารถ undo ได้เมื่อกด Undo ซึ่งแค่ต้องไปดึง ICommand ล่าสุดจาก stack แล้วสั่ง .Undo() มันก็จะคืนค่า HP และ AP ที่ได้ใช้ไปตามที่เขียน logic เอาไว้ตามในแต่ละคำสั่ง


<hr>


<H3>7. Observer Pattern </H3>

<h4>interface IGameEvent</h4>
<img width="368" height="121" alt="image" src="https://github.com/user-attachments/assets/f2de028d-8d0f-470b-8b7f-b9d0f10ccf97" />
<h4>Example CharacterActionTakenEvent</h4>
<img width="682" height="242" alt="image" src="https://github.com/user-attachments/assets/a97ac7e4-20c3-4555-aff9-bbb72115eccc" />
<h4>Example CharacterDamageTakenEvent</h4>
<img width="670" height="155" alt="image" src="https://github.com/user-attachments/assets/119e1ae9-523d-48d5-a4b4-98ee7c9788fe" />
<h4>Example EnemyCreatedEvent</h4>
<img width="569" height="190" alt="image" src="https://github.com/user-attachments/assets/59c09117-b361-44e6-bb53-22d29d8d23bc" />
<h4>Example OnVictoryEvent</h4>
<img width="511" height="171" alt="image" src="https://github.com/user-attachments/assets/40460f20-7238-4204-9b01-b7e5be384683" />
<h4>Example TurnPhaseChange</h4>
<img width="603" height="148" alt="image" src="https://github.com/user-attachments/assets/88fb2bb6-7cb4-4785-bb36-0c22fe7f553e" />

<br>
<br>
  เป็นระบบประกาศข้อมูล publish และ รับข้อมูล subscribe เหมือนกับการทำ notification โดยที่คนประกาศไม่ต้องรู้ว่ามีใครฟังอยู่บ้าง แต่หากว่าสมัครรับข้อมูลหรือแจ้งเตือนไว้ ก็จะสามารถเข้าถึงข้อมูลนั้นได้ 
ประโยชน์: ช่วยลด decoupling ระหว่างระบบ script ได้
ตัวอย่างในโปรเจกต์:
EventManager: เป็นตัวกลางกระจายข้อมูล
Event Data: เช่น EnemyCreatedEvent, CharacterActionTakenEvent จะเป็นข้อมูลหรือเหตุการณ์ที่ถูกประกาศออกไป
เมื่อ EnemyFactory สร้างศัตรูเสร็จ มันจะบอกผ่าน EventManager ว่าสร้างเสร็จแล้วนะ โดยการ publish EnemyCreateEvent แล้วให้ข้อมูลตาม Event Data เช่น สร้างตัวอะไร เลือดเท่าไร เป็นต้น หรืออาจไม่ต้องมีข้อมูลอะไรก็ได้นอกจากบอกว่าสร้างเสร็จแล้ว แล้วทางฝั่ง UIManager ที่คอยฟังโดย subscrive EnemyCreatedEvent อยู่ ก็จะมาอัปเดตชื่อและหลอดเลือดบนหน้าจอตามข้อมูลที่มากับ Event Data ทำให้ต้องทำงานแค่ตอนมีข้อมูลเข้ามา


<hr>


<H3>8. Bridge Pattern</H3>

<h4>class Chracacter</h4>
<img width="725" height="251" alt="image" src="https://github.com/user-attachments/assets/332e8f79-3187-483b-93f4-c0e94ff49fe5" />
<h4>interface IWeapon</h4>
<img width="433" height="196" alt="image" src="https://github.com/user-attachments/assets/54a54d60-3f1a-4c42-b006-93efc03fcbd3" />
<h4>class WeaponConfig as ScriptableOPbject</h4>
<img width="911" height="233" alt="image" src="https://github.com/user-attachments/assets/df5c15a6-eb49-4ade-b7b1-435cb54a6b36" />

<br>
<br>
  เป็นการแยก abstraction ออกจากการ implement เพื่อให้ทั้งสองส่วนพัฒนาแยกกันได้ 
ประโยชน์: ทำให้สามารถจัดการและเพิ่มข้อมูลส่วนต่างๆ ได้โดยไม่กระทบต่อกัน เช่นตัวละครสามารถเปลี่ยนอาวุธหรืออุปกรณ์ได้หลากหลายโดยไม่ต้องแก้โค้ดที่ตัวละคร และอาวุธก็มี Logic ของตัวเองได้ 
ตัวอย่างในโปรเจกต์:
Character และ IWeapon: ตัวละครหรือ Character ถือจะมี IWeapon เป็นของตัวเอง แล้วเวลาเรียกใช้งานจากใน AttackCommand จะมีการสั่ง source.weapon.CalculateDamage() ตัวละครไม่ต้องรู้ว่าตัวเองมีอาวุธชนิดไหน อาวุธอะไร เป็นดาบหรือปืน รู้แค่ว่าอาวุธนี้สามารถเรียกใช้ CalculateDamage() คำนวณหาดาเมจออกมาได้ก็พอ ทำให้เราสร้าง PhysicalWeapon หรือ ElementalWeapon มาเป็นอาวุธอะไรก็ได้และสวมใส่ให้ใครก็ได้

