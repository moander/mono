// cs0619-14.cs: `AA' is obsolete: `'
// Line: 17

class A
{
}

[System.Obsolete ("", true)]
class AA: A
{
        public void Foo () {}
}

class B {
        public B (A a)
        {
                (a as AA).Foo ();
        }
}