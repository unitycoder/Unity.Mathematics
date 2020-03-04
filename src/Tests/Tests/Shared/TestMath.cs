using NUnit.Framework;
using static Unity.Mathematics.math;
using Burst.Compiler.IL.Tests;
using System;

namespace Unity.Mathematics.Tests
{
    [TestFixture]
    public partial class TestMath
    {
        [TestCompiler]
        public static void asint_uint()
        {
            TestUtils.AreEqual(0, asint(0u));
            TestUtils.AreEqual(0x12345678, asint(0x12345678u));
            TestUtils.AreEqual(0x7FFFFFFF, asint(0x7FFFFFFFu));
            TestUtils.AreEqual(-2147483648, asint(0x80000000u));
            TestUtils.AreEqual(-2023406815, asint(0x87654321u));
            TestUtils.AreEqual(-1, asint(0xFFFFFFFFu));
        }

        [TestCompiler]
        public static void asint_uint2()
        {
            TestUtils.AreEqual(int2(0, 0x12345678), asint(uint2(0u, 0x12345678u)));
            TestUtils.AreEqual(int2(0x7FFFFFFF, -2147483648), asint(uint2(0x7FFFFFFFu, 0x80000000u)));
            TestUtils.AreEqual(int2(-2023406815, -1), asint(uint2(0x87654321u, 0xFFFFFFFFu)));
        }

        [TestCompiler]
        public static void asint_uint3()
        {
            TestUtils.AreEqual(int3(0, 0x12345678, 0x7FFFFFFF), asint(uint3(0u, 0x12345678u, 0x7FFFFFFFu)));
            TestUtils.AreEqual(int3(-2147483648, -2023406815, -1), asint(uint3(0x80000000u, 0x87654321u, 0xFFFFFFFFu)));
        }

        [TestCompiler]
        public static void asint_uint4()
        {
            TestUtils.AreEqual(int4(0, 0x12345678, 0x7FFFFFFF, -2147483648), asint(uint4(0u, 0x12345678u, 0x7FFFFFFFu, 0x80000000u)));
            TestUtils.AreEqual(int4(-2023406815, -1, 0, 0), asint(uint4(0x87654321u, 0xFFFFFFFFu, 0u, 0u)));
        }

        [TestCompiler]
        public static void asint_float()
        {
            TestUtils.AreEqual(0, asint(0.0f));
            TestUtils.AreEqual(0x3F800000, asint(1.0f));
            TestUtils.AreEqual(0x449A51EC, asint(1234.56f));
            TestUtils.AreEqual(0x7F800000, asint(float.PositiveInfinity));
            TestUtils.AreEqual(unchecked((int)0xFFC00000), asint(float.NaN));

            TestUtils.AreEqual(unchecked((int)0xBF800000), asint(-1.0f));
            TestUtils.AreEqual(unchecked((int)0xC49A51EC), asint(-1234.56f));
            TestUtils.AreEqual(unchecked((int)0xFF800000), asint(float.NegativeInfinity));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void asint_float_signed_zero()
        {
            TestUtils.AreEqual(unchecked((int)0x80000000), asint(-0.0f));
        }

        [TestCompiler]
        public static void asint_float2()
        {
            TestUtils.AreEqual(int2(0, 0x3F800000), asint(float2(0.0f, 1.0f)));
            TestUtils.AreEqual(int2(0x449A51EC, 0x7F800000), asint(float2(1234.56f, float.PositiveInfinity)));
            TestUtils.AreEqual(int2(unchecked((int)0xFFC00000), unchecked((int)0xBF800000)), asint(float2(float.NaN, -1.0f)));

            TestUtils.AreEqual(int2(unchecked((int)0xC49A51EC), unchecked((int)0xFF800000)), asint(float2(-1234.56f, float.NegativeInfinity)));
            TestUtils.AreEqual(int2(0, 0), asint(float2(0.0f, 0.0f)));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void asint_float2_signed_zero()
        {
            TestUtils.AreEqual(int2(unchecked((int)0x80000000), unchecked((int)0x80000000)), asint(float2(-0.0f, -0.0f)));
        }

        [TestCompiler]
        public static void asint_float3()
        {
            TestUtils.AreEqual(int3(0, 0x3F800000, 0x449A51EC), asint(float3(0.0f, 1.0f, 1234.56f)));
            TestUtils.AreEqual(int3(0x7F800000, unchecked((int)0xFFC00000), unchecked((int)0xBF800000)), asint(float3(float.PositiveInfinity, float.NaN, -1.0f)));
            TestUtils.AreEqual(int3(unchecked((int)0xC49A51EC), unchecked((int)0xFF800000), 0), asint(float3(-1234.56f, float.NegativeInfinity, 0.0f)));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void asint_float3_signed_zero()
        {
            TestUtils.AreEqual(int3(unchecked((int)0x80000000), unchecked((int)0x80000000), unchecked((int)0x80000000)), asint(float3(-0.0f, -0.0f, -0.0f)));
        }

        [TestCompiler]
        public static void asint_float4()
        {
            TestUtils.AreEqual(int4(0, 0x3F800000, 0x449A51EC, 0x7F800000), asint(float4(0.0f, 1.0f, 1234.56f, float.PositiveInfinity)));
            TestUtils.AreEqual(int4(unchecked((int)0xFFC00000), unchecked((int)0xBF800000), unchecked((int)0xC49A51EC), unchecked((int)0xFF800000)), asint(float4(float.NaN, -1.0f, -1234.56f, float.NegativeInfinity)));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void asint_float4_signed_zero()
        {
            TestUtils.AreEqual(int4(unchecked((int)0x80000000), unchecked((int)0x80000000), unchecked((int)0x80000000), unchecked((int)0x80000000)), asint(float4(-0.0f, -0.0f, -0.0f, -0.0f)));
        }

        [TestCompiler]
        public static void asuint_int()
        {
            TestUtils.AreEqual(0u, asuint(0));
            TestUtils.AreEqual(0x12345678u, asuint(0x12345678));
            TestUtils.AreEqual(0x7FFFFFFFu, asuint(0x7FFFFFFF));
            TestUtils.AreEqual(0x80000000u, asuint(-2147483648));
            TestUtils.AreEqual(0x87654321u, asuint(-2023406815));
            TestUtils.AreEqual(0xFFFFFFFFu, asuint(-1));
        }

        [TestCompiler]
        public static void asuint_int2()
        {
            TestUtils.AreEqual(uint2(0u, 0x12345678u), asuint(int2(0, 0x12345678)));
            TestUtils.AreEqual(uint2(0x7FFFFFFFu, 0x80000000u), asuint(int2(0x7FFFFFFF, -2147483648)));
            TestUtils.AreEqual(uint2(0x87654321u, 0xFFFFFFFFu), asuint(int2(-2023406815, -1)));
        }

        [TestCompiler]
        public static void asuint_int3()
        {
            TestUtils.AreEqual(uint3(0u, 0x12345678u, 0x7FFFFFFFu), asuint(int3(0, 0x12345678, 0x7FFFFFFF)));
            TestUtils.AreEqual(uint3(0x80000000u, 0x87654321u, 0xFFFFFFFFu), asuint(int3(-2147483648, -2023406815, -1)));
        }

        [TestCompiler]
        public static void asuint_int4()
        {
            TestUtils.AreEqual(uint4(0u, 0x12345678u, 0x7FFFFFFFu, 0x80000000u), asuint(int4(0, 0x12345678, 0x7FFFFFFF, -2147483648)));
            TestUtils.AreEqual(uint4(0x87654321u, 0xFFFFFFFFu, 0u, 0u), asuint(int4(-2023406815, -1, 0, 0)));
        }

        [TestCompiler]
        public static void asuint_float()
        {
            TestUtils.AreEqual(0u, asuint(0.0f));
            TestUtils.AreEqual(0x3F800000u, asuint(1.0f));
            TestUtils.AreEqual(0x449A51ECu, asuint(1234.56f));
            TestUtils.AreEqual(0x7F800000u, asuint(float.PositiveInfinity));
            TestUtils.AreEqual(0xFFC00000u, asuint(float.NaN));

            TestUtils.AreEqual(0xBF800000u, asuint(-1.0f));
            TestUtils.AreEqual(0xC49A51ECu, asuint(-1234.56f));
            TestUtils.AreEqual(0xFF800000u, asuint(float.NegativeInfinity));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void asuint_float_signed_zero()
        {
            TestUtils.AreEqual(0x80000000u, asuint(-0.0f));
        }

        [TestCompiler]
        public static void asuint_float2()
        {
            TestUtils.AreEqual(uint2(0u, 0x3F800000u), asuint(float2(0.0f, 1.0f)));
            TestUtils.AreEqual(uint2(0x449A51Ecu, 0x7F800000u), asuint(float2(1234.56f, float.PositiveInfinity)));
            TestUtils.AreEqual(uint2(0xFFC00000u, 0xBF800000u), asuint(float2(float.NaN, -1.0f)));

            TestUtils.AreEqual(uint2(0xC49A51ECu, 0xFF800000u), asuint(float2(-1234.56f, float.NegativeInfinity)));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void asuint_float2_signed_zero()
        {
            TestUtils.AreEqual(uint2(0x80000000u, 0x80000000u), asuint(float2(-0.0f, -0.0f)));
        }

        [TestCompiler]
        public static void asuint_float3()
        {
            TestUtils.AreEqual(uint3(0u, 0x3F800000u, 0x449A51ECu), asuint(float3(0.0f, 1.0f, 1234.56f)));
            TestUtils.AreEqual(uint3(0x7F800000u, 0xFFC00000u, 0xBF800000u), asuint(float3(float.PositiveInfinity, float.NaN, -1.0f)));
            TestUtils.AreEqual(uint3(0xC49A51ECu, 0xff800000u, 0u), asuint(float3(-1234.56f, float.NegativeInfinity, 0.0f)));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void asuint_float3_signed_zero()
        {
            TestUtils.AreEqual(uint3(0x80000000u, 0x80000000u, 0x80000000u), asuint(float3(-0.0f, -0.0f, -0.0f)));
        }

        [TestCompiler]
        public static void asuint_float4()
        {
            TestUtils.AreEqual(uint4(0u, 0x3F800000u, 0x449A51ECu, 0x7F800000u), asuint(float4(0.0f, 1.0f, 1234.56f, float.PositiveInfinity)));
            TestUtils.AreEqual(uint4(0xFFC00000u, 0xBF800000u, 0xC49A51ECu, 0xFF800000u), asuint(float4(float.NaN, -1.0f, -1234.56f, float.NegativeInfinity)));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void asuint_float4_singed_zero()
        {
            TestUtils.AreEqual(uint4(0x80000000u, 0x80000000u, 0x80000000u, 0x80000000u), asuint(float4(-0.0f, -0.0f, -0.0f, -0.0f)));
        }

        [TestCompiler]
        public static void aslong_ulong()
        {
            TestUtils.AreEqual(0L, aslong(0ul));
            TestUtils.AreEqual(0x0123456789ABCDEFL, aslong(0x0123456789ABCDEFul));
            TestUtils.AreEqual(0x7FFFFFFFFFFFFFFFL, aslong(0x7FFFFFFFFFFFFFFFul));
            TestUtils.AreEqual(-9223372036854775808L, aslong(0x8000000000000000ul));
            TestUtils.AreEqual(-81985529216486896L, aslong(0xFEDCBA9876543210ul));
            TestUtils.AreEqual(-1L, aslong(0xFFFFFFFFFFFFFFFFul));
        }

        [TestCompiler]
        public static void aslong_double()
        {
            TestUtils.AreEqual(0L, aslong(0.0));
            TestUtils.AreEqual(0x3FF0000000000000L, aslong(1.0));
            TestUtils.AreEqual(0x40934A3D70A3D70AL, aslong(1234.56));
            TestUtils.AreEqual(0x7FF0000000000000L, aslong(double.PositiveInfinity));
            TestUtils.AreEqual(unchecked((long)0xFFF8000000000000UL), aslong(double.NaN));

            TestUtils.AreEqual(unchecked((long)0xBFF0000000000000UL), aslong(-1.0));
            TestUtils.AreEqual(unchecked((long)0xC0934A3D70A3D70AUL), aslong(-1234.56));
            TestUtils.AreEqual(unchecked((long)0xFFF0000000000000UL), aslong(double.NegativeInfinity));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void aslong_double_signed_zero()
        {
            TestUtils.AreEqual(unchecked((long)0x8000000000000000UL), aslong(-0.0));
        }

        [TestCompiler]
        public static void asulong_long()
        {
            TestUtils.AreEqual(0ul, asulong(0L));
            TestUtils.AreEqual(0x0123456789ABCDEFul, asulong(0x0123456789ABCDEFL));
            TestUtils.AreEqual(0x7FFFFFFFFFFFFFFFul, asulong(0x7FFFFFFFFFFFFFFFL));
            TestUtils.AreEqual(0x8000000000000000ul, asulong(-9223372036854775808L));
            TestUtils.AreEqual(0xFEDCBA9876543210ul, asulong(-81985529216486896L));
            TestUtils.AreEqual(0xFFFFFFFFFFFFFFFFul, asulong(-1L));
        }

        [TestCompiler]
        public static void asulong_double()
        {
            TestUtils.AreEqual(0UL, asulong(0.0));
            TestUtils.AreEqual(0x3FF0000000000000UL, asulong(1.0));
            TestUtils.AreEqual(0x40934A3D70A3D70AUL, asulong(1234.56));
            TestUtils.AreEqual(0x7FF0000000000000UL, asulong(double.PositiveInfinity));
            TestUtils.AreEqual(0xFFF8000000000000UL, asulong(double.NaN));

            TestUtils.AreEqual(0xBFF0000000000000UL, asulong(-1.0));
            TestUtils.AreEqual(0xC0934A3D70A3D70AUL, asulong(-1234.56));
            TestUtils.AreEqual(0xFFF0000000000000UL, asulong(double.NegativeInfinity));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void asulong_double_signed_zero()
        {
            TestUtils.AreEqual(0x8000000000000000UL, asulong(-0.0));
        }

        [TestCompiler]
        public static void asfloat_int()
        {
            TestUtils.AreEqual(0.0f, asfloat(0));
            TestUtils.AreEqual(1.0f, asfloat(0x3F800000));
            TestUtils.AreEqual(1234.56f, asfloat(0x449A51EC));
            TestUtils.AreEqual(float.PositiveInfinity, asfloat(0x7F800000));
            TestUtils.AreEqual(-0.0f, asfloat(unchecked((int)0x80000000)));
            TestUtils.AreEqual(-1.0f, asfloat(unchecked((int)0xBF800000)));
            TestUtils.AreEqual(-1234.56f, asfloat(unchecked((int)0xC49A51EC)));
            TestUtils.AreEqual(float.NegativeInfinity, asfloat(unchecked((int)0xFF800000)));

            TestUtils.AreEqual(asuint(float.NaN), asuint(asfloat(unchecked((int)0xFFC00000))));
        }

        [TestCompiler]
        public static void asfloat_int2()
        {
            TestUtils.AreEqual(float2(0.0f, 1.0f), asfloat(int2(0, 0x3F800000)));
            TestUtils.AreEqual(float2(1234.56f, float.PositiveInfinity), asfloat(int2(0x449A51EC, 0x7F800000)));
            TestUtils.AreEqual(float2(-0.0f, -1.0f), asfloat(int2(unchecked((int)0x80000000), unchecked((int)0xBF800000))));

            TestUtils.AreEqual(float2(-1234.56f, float.NegativeInfinity), asfloat(int2(unchecked((int)0xC49A51EC), unchecked((int)0xFF800000))));
            TestUtils.AreEqual(asuint(float2(float.NaN, float.NaN)), asuint(asfloat(int2(unchecked((int)0xFFC00000), unchecked((int)0xFFC00000)))));
        }

        [TestCompiler]
        public static void asfloat_int3()
        {
            TestUtils.AreEqual(float3(0.0f, 1.0f, 1234.56f), asfloat(int3(0, 0x3F800000, 0x449A51EC)));
            TestUtils.AreEqual(float3(float.PositiveInfinity, -0.0f, -1.0f), asfloat(int3(0x7F800000, unchecked((int)0x80000000), unchecked((int)0xBF800000))));

            TestUtils.AreEqual(float3(-1234.56f, float.NegativeInfinity, 0.0f), asfloat(int3(unchecked((int)0xC49A51EC), unchecked((int)0xFF800000), 0)));
            TestUtils.AreEqual(asuint(float3(float.NaN, float.NaN, float.NaN)), asuint(asfloat(int3(unchecked((int)0xFFC00000), unchecked((int)0xFFC00000), unchecked((int)0xFFC00000)))));
        }

        [TestCompiler]
        public static void asfloat_int4()
        {
            TestUtils.AreEqual(float4(0.0f, 1.0f, 1234.56f, float.PositiveInfinity), asfloat(int4(0, 0x3F800000, 0x449A51EC, 0x7F800000)));
            TestUtils.AreEqual(float4(-0.0f, -1.0f, -1234.56f, float.NegativeInfinity), asfloat(int4(unchecked((int)0x80000000), unchecked((int)0xBF800000), unchecked((int)0xC49A51EC), unchecked((int)0xFF800000))));

            TestUtils.AreEqual(asuint(float4(float.NaN, float.NaN, float.NaN, float.NaN)), asuint(asfloat(int4(unchecked((int)0xFFC00000), unchecked((int)0xFFC00000), unchecked((int)0xFFC00000), unchecked((int)0xFFC00000)))));
        }

        [TestCompiler]
        public static void asfloat_uint()
        {
            TestUtils.AreEqual(0.0f, asfloat(0u));
            TestUtils.AreEqual(1.0f, asfloat(0x3F800000u));
            TestUtils.AreEqual(1234.56f, asfloat(0x449A51ECu));
            TestUtils.AreEqual(float.PositiveInfinity, asfloat(0x7F800000u));
            TestUtils.AreEqual(-0.0f, asfloat(0x80000000u));
            TestUtils.AreEqual(-1.0f, asfloat(0xBF800000u));
            TestUtils.AreEqual(-1234.56f, asfloat(0xC49A51ECu));
            TestUtils.AreEqual(float.NegativeInfinity, asfloat(0xFF800000u));

            TestUtils.AreEqual(asuint(float.NaN), asuint(asfloat(0xFFC00000u)));
        }

        [TestCompiler]
        public static void asfloat_uint2()
        {
            TestUtils.AreEqual(float2(0.0f, 1.0f), asfloat(uint2(0u, 0x3F800000u)));
            TestUtils.AreEqual(float2(1234.56f, float.PositiveInfinity), asfloat(uint2(0x449A51ECu, 0x7F800000u)));
            TestUtils.AreEqual(float2(-0.0f, -1.0f), asfloat(uint2(0x80000000u, 0xBF800000u)));

            TestUtils.AreEqual(float2(-1234.56f, float.NegativeInfinity), asfloat(uint2(0xC49A51ECu, 0xFF800000u)));
            TestUtils.AreEqual(asuint(float2(float.NaN, float.NaN)), asuint(asfloat(uint2(0xFFC00000u, 0xFFC00000u))));
        }

        [TestCompiler]
        public static void asfloat_uint3()
        {
            TestUtils.AreEqual(float3(0.0f, 1.0f, 1234.56f), asfloat(uint3(0u, 0x3F800000u, 0x449A51ECu)));
            TestUtils.AreEqual(float3(float.PositiveInfinity, -0.0f, -1.0f), asfloat(uint3(0x7F800000u, 0x80000000u, 0xBF800000u)));

            TestUtils.AreEqual(float3(-1234.56f, float.NegativeInfinity, 0.0f), asfloat(uint3(0xC49A51ECu, 0xFF800000u, 0u)));
            TestUtils.AreEqual(asuint(float3(float.NaN, float.NaN, float.NaN)), asuint(asfloat(uint3(0xFFC00000u, 0xFFC00000u, 0xFFC00000u))));
        }

        [TestCompiler]
        public static void asfloat_uint4()
        {
            TestUtils.AreEqual(float4(0.0f, 1.0f, 1234.56f, float.PositiveInfinity), asfloat(uint4(0u, 0x3F800000u, 0x449A51ECu, 0x7F800000u)));
            TestUtils.AreEqual(float4(-0.0f, -1.0f, -1234.56f, float.NegativeInfinity), asfloat(uint4(0x80000000u, 0xBF800000u, 0xC49A51ECu, 0xFF800000u)));

            TestUtils.AreEqual(asuint(float4(float.NaN, float.NaN, float.NaN, float.NaN)), asuint(asfloat(uint4(0xFFC00000u, 0xFFC00000u, 0xFFC00000u, 0xFFC00000u))));
        }

        [TestCompiler]
        public static void asdouble_long()
        {
            TestUtils.AreEqual(0.0, asdouble(0L));
            TestUtils.AreEqual(1.0, asdouble(0x3FF0000000000000L));
            TestUtils.AreEqual(1234.56, asdouble(0x40934A3D70A3D70AL));
            TestUtils.AreEqual(double.PositiveInfinity, asdouble(0x7FF0000000000000L));
            TestUtils.AreEqual(double.NaN, asdouble(unchecked((long)0xFFF8000000000000UL)));

            TestUtils.AreEqual(-0.0, asdouble(unchecked((long)0x8000000000000000UL)));
            TestUtils.AreEqual(-1.0, asdouble(unchecked((long)0xBFF0000000000000UL)));
            TestUtils.AreEqual(-1234.56, asdouble(unchecked((long)0xC0934A3D70A3D70AUL)));
            TestUtils.AreEqual(double.NegativeInfinity, asdouble(unchecked((long)0xFFF0000000000000UL)));
        }

        [TestCompiler]
        public static void asdouble_ulong()
        {
            TestUtils.AreEqual(0.0, asdouble(0UL));
            TestUtils.AreEqual(1.0, asdouble(0x3FF0000000000000UL));
            TestUtils.AreEqual(1234.56, asdouble(0x40934A3D70A3D70AUL));
            TestUtils.AreEqual(double.PositiveInfinity, asdouble(0x7FF0000000000000UL));
            TestUtils.AreEqual(double.NaN, asdouble(0xFFF8000000000000UL));

            TestUtils.AreEqual(-0.0, asdouble(0x8000000000000000UL));
            TestUtils.AreEqual(-1.0, asdouble(0xBFF0000000000000UL));
            TestUtils.AreEqual(-1234.56, asdouble(0xC0934A3D70A3D70AUL));
            TestUtils.AreEqual(double.NegativeInfinity, asdouble(0xFFF0000000000000UL));
        }

        [TestCompiler]
        public static void faceforward_float2()
        {
            TestUtils.AreEqual(float2(-3.5f, 4.5f), faceforward(float2(3.5f, -4.5f), float2(1.0f, -2.0f), float2(3.0f, -4.0f)));
            TestUtils.AreEqual(float2(3.5f, -4.5f), faceforward(float2(3.5f, -4.5f), float2(1.0f, -2.0f), float2(-3.0f, 4.0f)));
            TestUtils.AreEqual(float2(-3.5f, 4.5f), faceforward(float2(3.5f, -4.5f), float2(1.0f, -2.0f), float2(0.0f, 0.0f)));
        }

        [TestCompiler]
        public static void faceforward_float3()
        {
            TestUtils.AreEqual(float3(-3.5f, 4.5f, -5.5f), faceforward(float3(3.5f, -4.5f, 5.5f), float3(1.0f, -2.0f, 3.0f), float3(3.0f, -4.0f, 5.0f)));
            TestUtils.AreEqual(float3(3.5f, -4.5f, 5.5f), faceforward(float3(3.5f, -4.5f, 5.5f), float3(1.0f, -2.0f, 3.0f), float3(-3.0f, 4.0f, -5.0f)));
            TestUtils.AreEqual(float3(-3.5f, 4.5f, -5.5f), faceforward(float3(3.5f, -4.5f, 5.5f), float3(1.0f, -2.0f, 3.0f), float3(0.0f, 0.0f, 0.0f)));
        }

        [TestCompiler]
        public static void faceforward_float4()
        {
            TestUtils.AreEqual(float4(-3.5f, 4.5f, -5.5f, 6.5f), faceforward(float4(3.5f, -4.5f, 5.5f, -6.5f), float4(1.0f, -2.0f, 3.0f, -4.0f), float4(3.0f, -4.0f, 5.0f, -6.0f)));
            TestUtils.AreEqual(float4(3.5f, -4.5f, 5.5f, -6.5f), faceforward(float4(3.5f, -4.5f, 5.5f, -6.5f), float4(1.0f, -2.0f, 3.0f, -4.0f), float4(-3.0f, 4.0f, -5.0f, 6.0f)));
            TestUtils.AreEqual(float4(-3.5f, 4.5f, -5.5f, 6.5f), faceforward(float4(3.5f, -4.5f, 5.5f, -6.5f), float4(1.0f, -2.0f, 3.0f, -4.0f), float4(0.0f, 0.0f, 0.0f, 0.0f)));
        }

        [TestCompiler]
        public static void faceforward_double2()
        {
            TestUtils.AreEqual(double2(-3.5, 4.5), faceforward(double2(3.5, -4.5), double2(1.0, -2.0), double2(3.0, -4.0)));
            TestUtils.AreEqual(double2(3.5, -4.5), faceforward(double2(3.5, -4.5), double2(1.0, -2.0), double2(-3.0, 4.0)));
            TestUtils.AreEqual(double2(-3.5, 4.5), faceforward(double2(3.5, -4.5), double2(1.0, -2.0), double2(0.0, 0.0)));
        }

        [TestCompiler]
        public static void faceforward_double3()
        {
            TestUtils.AreEqual(double3(-3.5, 4.5, -5.5), faceforward(double3(3.5, -4.5, 5.5), double3(1.0, -2.0, 3.0), double3(3.0, -4.0, 5.0)));
            TestUtils.AreEqual(double3(3.5, -4.5, 5.5), faceforward(double3(3.5, -4.5, 5.5), double3(1.0, -2.0, 3.0), double3(-3.0, 4.0, -5.0)));
            TestUtils.AreEqual(double3(-3.5, 4.5, -5.5), faceforward(double3(3.5, -4.5, 5.5), double3(1.0, -2.0, 3.0), double3(0.0, 0.0, 0.0)));
        }

        [TestCompiler]
        public static void faceforward_double4()
        {
            TestUtils.AreEqual(double4(-3.5, 4.5, -5.5, 6.5), faceforward(double4(3.5, -4.5, 5.5, -6.5), double4(1.0, -2.0, 3.0, -4.0), double4(3.0, -4.0, 5.0, -6.0)));
            TestUtils.AreEqual(double4(3.5, -4.5, 5.5, -6.5), faceforward(double4(3.5, -4.5, 5.5, -6.5), double4(1.0, -2.0, 3.0, -4.0), double4(-3.0, 4.0, -5.0, 6.0)));
            TestUtils.AreEqual(double4(-3.5, 4.5, -5.5, 6.5), faceforward(double4(3.5, -4.5, 5.5, -6.5), double4(1.0, -2.0, 3.0, -4.0), double4(0.0, 0.0, 0.0, 0.0)));
        }

        [TestCompiler]
        public static void modf_float()
        {
            float f, i;
            f = modf(313.75f, out i);
            TestUtils.AreEqual(313.0f, i);
            TestUtils.AreEqual(0.75f, f);

            f = modf(-313.25f, out i);
            TestUtils.AreEqual(-313.0f, i);
            TestUtils.AreEqual(-0.25f, f);

            f = modf(-314.0f, out i);
            TestUtils.AreEqual(-314.0f, i);
            TestUtils.AreEqual(0.0f, f);
        }

        [TestCompiler]
        public static void modf_float2()
        {
            float2 f, i;
            f = modf(float2(313.75f, -313.25f), out i);
            TestUtils.AreEqual(float2(313.0f, -313.0f), i);
            TestUtils.AreEqual(float2(0.75f, -0.25f), f);

            f = modf(float2(-314.0f, 7.5f), out i);
            TestUtils.AreEqual(float2(-314.0f, 7.0f), i);
            TestUtils.AreEqual(float2(0.0f, 0.5f), f);
        }

        [TestCompiler]
        public static void modf_float3()
        {
            float3 f, i;
            f = modf(float3(313.75f, -313.25f, -314.0f), out i);
            TestUtils.AreEqual(float3(313.0f, -313.0f, -314.0f), i);
            TestUtils.AreEqual(float3(0.75f, -0.25f, 0.0f), f);
        }

        [TestCompiler]
        public static void modf_float4()
        {
            float4 f, i;
            f = modf(float4(313.75f, -313.25f, -314.0f, 7.5f), out i);
            TestUtils.AreEqual(float4(313.0f, -313.0f, -314.0f, 7.0f), i);
            TestUtils.AreEqual(float4(0.75f, -0.25f, 0.0f, 0.5f), f);
        }

        [TestCompiler]
        public static void modf_double()
        {
            double f, i;
            f = modf(313.75, out i);
            TestUtils.AreEqual(313.0, i);
            TestUtils.AreEqual(0.75, f);

            f = modf(-313.25, out i);
            TestUtils.AreEqual(-313.0, i);
            TestUtils.AreEqual(-0.25, f);

            f = modf(-314.0, out i);
            TestUtils.AreEqual(-314.0, i);
            TestUtils.AreEqual(0.0, f);
        }

        [TestCompiler]
        public static void modf_double2()
        {
            double2 f, i;
            f = modf(double2(313.75, -313.25), out i);
            TestUtils.AreEqual(double2(313.0, -313.0), i);
            TestUtils.AreEqual(double2(0.75, -0.25), f);

            f = modf(double2(-314.0, 7.5), out i);
            TestUtils.AreEqual(double2(-314.0, 7.0), i);
            TestUtils.AreEqual(double2(0.0, 0.5), f);
        }

        [TestCompiler]
        public static void modf_double3()
        {
            double3 f, i;
            f = modf(double3(313.75, -313.25, -314.0), out i);
            TestUtils.AreEqual(double3(313.0, -313.0, -314.0), i);
            TestUtils.AreEqual(double3(0.75, -0.25, 0.0), f);
        }

        [TestCompiler]
        public static void modf_double4()
        {
            double4 f, i;
            f = modf(double4(313.75, -313.25, -314.0, 7.5), out i);
            TestUtils.AreEqual(double4(313.0, -313.0, -314.0, 7.0), i);
            TestUtils.AreEqual(double4(0.75, -0.25, 0.0, 0.5f), f);
        }

        [TestCompiler]
        public static void normalize_float2()
        {
            TestUtils.AreEqual(float2(0.504883f, -0.863188f), normalize(float2(3.1f, -5.3f)), 0.0001f);
            TestUtils.AreEqual(true, all(isnan(normalize(float2(0.0f, 0.0f)))));
        }

        [TestCompiler]
        public static void normalize_float3()
        {
            TestUtils.AreEqual(float3(0.464916f, -0.794861f, 0.389932f), normalizesafe(float3(3.1f, -5.3f, 2.6f)), 0.0001f);
            TestUtils.AreEqual(true, all(isnan(normalize(float3(0.0f, 0.0f, 0.0f)))));
        }

        [TestCompiler]
        public static void normalize_float4()
        {
            TestUtils.AreEqual(float4(0.234727f, -0.401308f, 0.196868f, 0.863191f), normalizesafe(float4(3.1f, -5.3f, 2.6f, 11.4f)), 0.0001f);
            TestUtils.AreEqual(true, all(isnan(normalize(float4(0.0f, 0.0f, 0.0f, 0.0f)))));
        }


        [TestCompiler]
        public static void normalize_double2()
        {
            TestUtils.AreEqual(double2(0.504883, -0.863188), normalize(double2(3.1, -5.3)), 0.0001);
            TestUtils.AreEqual(true, all(isnan(normalize(double2(0.0, 0.0)))));
        }

        [TestCompiler]
        public static void normalize_double3()
        {
            TestUtils.AreEqual(double3(0.464916, -0.794861, 0.389932), normalizesafe(double3(3.1, -5.3, 2.6)), 0.0001);
            TestUtils.AreEqual(true, all(isnan(normalize(double3(0.0, 0.0, 0.0)))));
        }

        [TestCompiler]
        public static void normalize_double4()
        {
            TestUtils.AreEqual(double4(0.234727, -0.401308, 0.196868, 0.863191), normalizesafe(double4(3.1, -5.3, 2.6, 11.4)), 0.0001);
            TestUtils.AreEqual(true, all(isnan(normalize(double4(0.0, 0.0, 0.0, 0.0f)))));
        }

        [TestCompiler]
        public static void normalize_quaternion()
        {
            TestUtils.AreEqual(quaternion(0.234727f, -0.401308f, 0.196868f, 0.863191f), normalizesafe(quaternion(3.1f, -5.3f, 2.6f, 11.4f)), 0.0001f);
            TestUtils.AreEqual(true, all(isnan(normalize(quaternion(0.0f, 0.0f, 0.0f, 0.0f)).value)));
        }


        [TestCompiler]
        public static void normalizesafe_float2()
        {
            TestUtils.AreEqual(float2(0.504883f, -0.863188f), normalizesafe(float2(3.1f, -5.3f)), 0.0001f);
            TestUtils.AreEqual(float2(0.0f, 0.0f), normalizesafe(float2(0.0f, 0.0f)));
            TestUtils.AreEqual(float2(1.0f, 2.0f), normalizesafe(float2(0.0f, 0.0f), float2(1.0f, 2.0f)));
            TestUtils.AreEqual(float2(0.447214f, 0.894427f), normalizesafe(float2(1e-18f, 2e-18f)), 0.0001f);
            TestUtils.AreEqual(float2(1.0f, 2.0f), normalizesafe(float2(7.66e-20f, 7.66e-20f), float2(1.0f, 2.0f)));
        }

        [TestCompiler]
        public static void normalizesafe_float3()
        {
            TestUtils.AreEqual(float3(0.464916f, -0.794861f, 0.389932f), normalizesafe(float3(3.1f, -5.3f, 2.6f)), 0.0001f);
            TestUtils.AreEqual(float3(0.0f, 0.0f, 0.0f), normalizesafe(float3(0.0f, 0.0f, 0.0f)));
            TestUtils.AreEqual(float3(1.0f, 2.0f, 3.0f), normalizesafe(float3(0.0f, 0.0f, 0.0f), float3(1.0f, 2.0f, 3.0f)));
            TestUtils.AreEqual(float3(0.267261f, 0.534523f, 0.801784f), normalizesafe(float3(1e-19f, 2e-19f, 3e-19f)), 0.0001f);
            TestUtils.AreEqual(float3(1.0f, 2.0f, 3.0f), normalizesafe(float3(6.25e-20f, 6.25e-20f, 6.25e-20f), float3(1.0f, 2.0f, 3.0f)));
        }

        [TestCompiler]
        public static void normalizesafe_float4()
        {
            TestUtils.AreEqual(float4(0.234727f, -0.401308f, 0.196868f, 0.863191f), normalizesafe(float4(3.1f, -5.3f, 2.6f, 11.4f)), 0.0001f);
            TestUtils.AreEqual(float4(0.0f, 0.0f, 0.0f, 0.0f), normalizesafe(float4(0.0f, 0.0f, 0.0f, 0.0f)));
            TestUtils.AreEqual(float4(1.0f, 2.0f, 3.0f, 4.0f), normalizesafe(float4(0.0f, 0.0f, 0.0f, 0.0f), float4(1.0f, 2.0f, 3.0f, 4.0f)));
            TestUtils.AreEqual(float4(0.182574f, 0.3651484f, 0.547723f, 0.730297f), normalizesafe(float4(1e-19f, 2e-19f, 3e-19f, 4e-19f)), 0.0001f);
            TestUtils.AreEqual(float4(1.0f, 2.0f, 3.0f, 4.0f), normalizesafe(float4(5.42e-20f, 5.42e-20f, 5.42e-20f, 5.42e-20f), float4(1.0f, 2.0f, 3.0f, 4.0f)));
        }


        [TestCompiler]
        public static void normalizesafe_double2()
        {
            TestUtils.AreEqual(double2(0.504883, -0.863188), normalizesafe(double2(3.1, -5.3)), 0.0001);
            TestUtils.AreEqual(double2(0.0, 0.0), normalizesafe(double2(0.0, 0.0)));
            TestUtils.AreEqual(double2(1.0, 2.0), normalizesafe(double2(0.0, 0.0), double2(1.0, 2.0)));
            TestUtils.AreEqual(double2(0.447214, 0.894427), normalizesafe(double2(1e-18, 2e-18)), 0.0001);
            TestUtils.AreEqual(double2(1.0, 2.0), normalizesafe(double2(1.05e-154, 1.05e-154), double2(1.0, 2.0)));
        }

        [TestCompiler]
        public static void normalizesafe_double3()
        {
            TestUtils.AreEqual(double3(0.464916, -0.794861, 0.389932), normalizesafe(double3(3.1, -5.3, 2.6)), 0.0001);
            TestUtils.AreEqual(double3(0.0, 0.0, 0.0), normalizesafe(double3(0.0, 0.0, 0.0)));
            TestUtils.AreEqual(double3(1.0, 2.0, 3.0), normalizesafe(double3(0.0, 0.0, 0.0), double3(1.0, 2.0, 3.0)));
            TestUtils.AreEqual(double3(0.267261, 0.534523, 0.801784), normalizesafe(double3(1e-19, 2e-19, 3e-19)), 0.0001);
            TestUtils.AreEqual(double3(1.0, 2.0, 3.0), normalizesafe(double3(8.61e-155, 8.61e-155, 8.61e-155), double3(1.0, 2.0, 3.0)));
        }

        [TestCompiler]
        public static void normalizesafe_double4()
        {
            TestUtils.AreEqual(double4(0.234727, -0.401308, 0.196868, 0.863191), normalizesafe(double4(3.1, -5.3, 2.6, 11.4)), 0.0001);
            TestUtils.AreEqual(double4(0.0, 0.0, 0.0, 0.0), normalizesafe(double4(0.0, 0.0, 0.0, 0.0)));
            TestUtils.AreEqual(double4(1.0, 2.0, 3.0, 4.0), normalizesafe(double4(0.0, 0.0, 0.0, 0.0), double4(1.0, 2.0, 3.0, 4.0)));
            TestUtils.AreEqual(double4(0.182574, 0.3651484, 0.547723, 0.730297), normalizesafe(double4(1e-19, 2e-19, 3e-19, 4e-19)), 0.0001);
            TestUtils.AreEqual(double4(1.0, 2.0, 3.0, 4.0), normalizesafe(double4(7.45e-155, 7.45e-155, 7.45e-155, 7.45e-155), double4(1.0, 2.0, 3.0, 4.0)));
        }

        [TestCompiler]
        public static void normalizesafe_quaternion()
        {
            TestUtils.AreEqual(quaternion(0.234727f, -0.401308f, 0.196868f, 0.863191f), normalizesafe(quaternion(3.1f, -5.3f, 2.6f, 11.4f)), 0.0001f);
            TestUtils.AreEqual(quaternion(0.0f, 0.0f, 0.0f, 1.0f), normalizesafe(quaternion(0.0f, 0.0f, 0.0f, 0.0f)));
            TestUtils.AreEqual(quaternion(1.0f, 2.0f, 3.0f, 4.0f), normalizesafe(quaternion(0.0f, 0.0f, 0.0f, 0.0f), quaternion(1.0f, 2.0f, 3.0f, 4.0f)));
            TestUtils.AreEqual(quaternion(0.182574f, 0.3651484f, 0.547723f, 0.730297f), normalizesafe(quaternion(1e-19f, 2e-19f, 3e-19f, 4e-19f)), 0.0001f);
            TestUtils.AreEqual(quaternion(1.0f, 2.0f, 3.0f, 4.0f), normalizesafe(quaternion(5.42e-20f, 5.42e-20f, 5.42e-20f, 5.42e-20f), quaternion(1.0f, 2.0f, 3.0f, 4.0f)));
        }

        [TestCompiler]
        public static void f16tof32_float()
        {
            TestUtils.AreEqual(0x00000000, asuint(f16tof32(0x0000)));
            TestUtils.AreEqual(0x3800C000, asuint(f16tof32(0x0203)));
            TestUtils.AreEqual(0x40642000, asuint(f16tof32(0x4321)));
            TestUtils.AreEqual(0x477FE000, asuint(f16tof32(0x7BFF)));
            TestUtils.AreEqual(0x7F800000, asuint(f16tof32(0x7C00)));
            TestUtils.AreEqual(true, isnan(f16tof32(0x7C01)));

            TestUtils.AreEqual(0x80000000, asuint(f16tof32(0x8000)));
            TestUtils.AreEqual(0xB800C000, asuint(f16tof32(0x8203)));
            TestUtils.AreEqual(0xC0642000, asuint(f16tof32(0xC321)));
            TestUtils.AreEqual(0xC77FE000, asuint(f16tof32(0xFBFF)));
            TestUtils.AreEqual(0xFF800000, asuint(f16tof32(0xFC00)));
            TestUtils.AreEqual(true, isnan(f16tof32(0xFC01)));
        }

        [TestCompiler]
        public static void f16tof32_float2()
        {
            TestUtils.AreEqual(uint2(0x00000000, 0x3800C000), asuint(f16tof32(uint2(0x0000, 0x0203))));
            TestUtils.AreEqual(uint2(0x40642000, 0x477FE000), asuint(f16tof32(uint2(0x4321, 0x7BFF))));
            TestUtils.AreEqual(uint2(0x7F800000, 0x7F800000), asuint(f16tof32(uint2(0x7C00, 0x7C00))));
            TestUtils.AreEqual(true, all(isnan(f16tof32(uint2(0x7C01, 0x7C01)))));

            TestUtils.AreEqual(uint2(0x80000000, 0xB800C000), asuint(f16tof32(uint2(0x8000, 0x8203))));
            TestUtils.AreEqual(uint2(0xC0642000, 0xC77FE000), asuint(f16tof32(uint2(0xC321, 0xFBFF))));
            TestUtils.AreEqual(uint2(0xFF800000, 0xFF800000), asuint(f16tof32(uint2(0xFC00, 0xFC00))));
            TestUtils.AreEqual(true, all(isnan(f16tof32(uint2(0xFC01, 0xFC01)))));
        }

        [TestCompiler]
        public static void f16tof32_float3()
        {
            TestUtils.AreEqual(uint3(0x00000000, 0x3800C000, 0x40642000), asuint(f16tof32(uint3(0x0000, 0x0203, 0x4321))));
            TestUtils.AreEqual(uint3(0x477FE000, 0x7F800000, 0x7F800000), asuint(f16tof32(uint3(0x7BFF, 0x7C00, 0x7C00))));
            TestUtils.AreEqual(true, all(isnan(f16tof32(uint3(0x7C01, 0x7C01, 0x7C01)))));

            TestUtils.AreEqual(uint3(0x80000000, 0xB800C000, 0xC0642000), asuint(f16tof32(uint3(0x8000, 0x8203, 0xC321))));
            TestUtils.AreEqual(uint3(0xC77FE000, 0xFF800000, 0xFF800000), asuint(f16tof32(uint3(0xFBFF, 0xFC00, 0xFC00))));
            TestUtils.AreEqual(true, all(isnan(f16tof32(uint3(0xFC01, 0xFC01, 0xFC01)))));
        }

        [TestCompiler]
        public static void f16tof32_float4()
        {
            TestUtils.AreEqual(uint4(0x00000000, 0x3800C000, 0x40642000, 0x477FE000), asuint(f16tof32(uint4(0x0000, 0x0203, 0x4321, 0x7BFF))));
            TestUtils.AreEqual(uint4(0x7F800000, 0x7F800000, 0x7F800000, 0x7F800000), asuint(f16tof32(uint4(0x7C00, 0x7C00, 0x7C00, 0x7C00))));
            TestUtils.AreEqual(true, all(isnan(f16tof32(uint4(0x7C01, 0x7C01, 0x7C01, 0x7C01)))));

            TestUtils.AreEqual(uint4(0x80000000, 0xB800C000, 0xC0642000, 0xC77FE000), asuint(f16tof32(uint4(0x8000, 0x8203, 0xC321, 0xFBFF))));
            TestUtils.AreEqual(uint4(0xFF800000, 0xFF800000, 0xFF800000, 0xFF800000), asuint(f16tof32(uint4(0xFC00, 0xFC00, 0xFC00, 0xFC00))));
            TestUtils.AreEqual(true, all(isnan(f16tof32(uint4(0xFC01, 0xFC01, 0xFC01, 0xFC01)))));
        }

        [TestCompiler]
        public static void f32tof16_float()
        {
            TestUtils.AreEqual(0x0000, f32tof16(0.0f));
            TestUtils.AreEqual(0x0000, f32tof16(2.98e-08f));
            TestUtils.AreEqual(0x0001, f32tof16(5.96046448e-08f));
            TestUtils.AreEqual(0x57B6, f32tof16(123.4f));
            TestUtils.AreEqual(0x7BFF, f32tof16(65504.0f));
            TestUtils.AreEqual(0x7C00, f32tof16(65520.0f));
            TestUtils.AreEqual(0x7C00, f32tof16(float.PositiveInfinity));
            TestUtils.AreEqual(0xFE00, f32tof16(float.NaN));

            TestUtils.AreEqual(0x8000, f32tof16(-2.98e-08f));
            TestUtils.AreEqual(0x8001, f32tof16(-5.96046448e-08f));
            TestUtils.AreEqual(0xD7B6, f32tof16(-123.4f));
            TestUtils.AreEqual(0xFBFF, f32tof16(-65504.0f));
            TestUtils.AreEqual(0xFC00, f32tof16(-65520.0f));
            TestUtils.AreEqual(0xFC00, f32tof16(float.NegativeInfinity));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void f32tof16_float_signed_zero()
        {
            TestUtils.AreEqual(0x8000, f32tof16(-0.0f));
        }

        [TestCompiler]
        public static void f32tof16_float2()
        {
            TestUtils.AreEqual(uint2(0x0000, 0x0000), f32tof16(float2(0.0f, 2.98e-08f)));
            TestUtils.AreEqual(uint2(0x0001, 0x57B6), f32tof16(float2(5.96046448e-08f, 123.4f)));
            TestUtils.AreEqual(uint2(0x7BFF, 0x7C00), f32tof16(float2(65504.0f, 65520.0f)));
            TestUtils.AreEqual(uint2(0x7C00, 0xFE00), f32tof16(float2(float.PositiveInfinity, float.NaN)));

            TestUtils.AreEqual(uint2(0x8000, 0x8001), f32tof16(float2(-2.98e-08f, -5.96046448e-08f)));
            TestUtils.AreEqual(uint2(0xD7B6, 0xFBFF), f32tof16(float2(-123.4f, -65504.0f)));
            TestUtils.AreEqual(uint2(0xFC00, 0xFC00), f32tof16(float2(-65520.0f, float.NegativeInfinity)));
            TestUtils.AreEqual(uint2(0xFC00, 0x0000), f32tof16(float2(float.NegativeInfinity, 0.0f)));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void f32tof16_float2_signed_zero()
        {
            TestUtils.AreEqual(uint2(0x8000, 0x8000), f32tof16(float2(-0.0f, -0.0f)));
        }

        [TestCompiler]
        public static void f32tof16_float3()
        {
            TestUtils.AreEqual(uint3(0x0000, 0x0000, 0x0001), f32tof16(float3(0.0f, 2.98e-08f, 5.96046448e-08f)));
            TestUtils.AreEqual(uint3(0x57B6, 0x7BFF, 0x7C00), f32tof16(float3(123.4f, 65504.0f, 65520.0f)));
            TestUtils.AreEqual(uint3(0x7C00, 0xFE00, 0x8000), f32tof16(float3(float.PositiveInfinity, float.NaN, -2.98e-08f)));

            TestUtils.AreEqual(uint3(0x8001, 0xD7B6, 0xFBFF), f32tof16(float3(-5.96046448e-08f, -123.4f, -65504.0f)));
            TestUtils.AreEqual(uint3(0xFC00, 0xFC00, 0x0000), f32tof16(float3(-65520.0f, float.NegativeInfinity, 0.0f)));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void f32tof16_float3_signed_zero()
        {
            TestUtils.AreEqual(uint3(0x8000, 0x8000, 0x8000), f32tof16(float3(-0.0f, -0.0f, -0.0f)));
        }

        [TestCompiler]
        public static void f32tof16_float4()
        {
            TestUtils.AreEqual(uint4(0x0000, 0x0000, 0x0001, 0x57B6), f32tof16(float4(0.0f, 2.98e-08f, 5.96046448e-08f, 123.4f)));
            TestUtils.AreEqual(uint4(0x7BFF, 0x7C00, 0x7C00, 0xFE00), f32tof16(float4(65504.0f, 65520.0f, float.PositiveInfinity, float.NaN)));
            TestUtils.AreEqual(uint4(0x8000, 0x8001, 0xD7B6, 0xFBFF), f32tof16(float4(-2.98e-08f, -5.96046448e-08f, -123.4f, -65504.0f)));
            TestUtils.AreEqual(uint4(0xFC00, 0xFC00, 0xFC00, 0x0000), f32tof16(float4(-65520.0f, float.NegativeInfinity, float.NegativeInfinity, 0.0f)));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void f32tof16_float4_signed_zero()
        {
            TestUtils.AreEqual(uint4(0x8000, 0x8000, 0x8000, 0x8000), f32tof16(float4(-0.0f, -0.0f, -0.0f, -0.0f)));
        }

        [TestCompiler]
        public static void reflect_float2()
        {
            TestUtils.AreEqual(float2(9.84f, -3.888f), reflect(float2(1.2f, 3.6f), float2(1.5f, -1.3f)), 8, false);
            TestUtils.AreEqual(float2(-9.84f, -3.888f), reflect(float2(-1.2f, 3.6f), float2(-1.5f, -1.3f)), 8, false);
            TestUtils.AreEqual(float2(-1.2f, 3.6f), reflect(float2(-1.2f, 3.6f), float2(0.0f, 0.0f)));
            TestUtils.AreEqual(float2(0.0f, 0.0f), reflect(float2(0.0f, 0.0f), float2(-1.5f, -1.3f)), 8, false);
        }

        [TestCompiler]
        public static void reflect_float3()
        {
            TestUtils.AreEqual(float3(35.88f, -26.456f, 68.872f), reflect(float3(1.2f, 3.6f, -2.8f), float3(1.5f, -1.3f, 3.1f)), 8, false);
            TestUtils.AreEqual(float3(-35.88f, -26.456f, 68.872f), reflect(float3(-1.2f, 3.6f, -2.8f), float3(-1.5f, -1.3f, 3.1f)), 8, false);
            TestUtils.AreEqual(float3(-1.2f, 3.6f, -2.8f), reflect(float3(-1.2f, 3.6f, -2.8f), float3(0.0f, 0.0f, 0.0f)));
            TestUtils.AreEqual(float3(0.0f, 0.0f, 0.0f), reflect(float3(0.0f, 0.0f, 0.0f), float3(-1.5f, -1.3f, 3.1f)), 8, false);
        }

        [TestCompiler]
        public static void reflect_float4()
        {
            TestUtils.AreEqual(float4(36.51f, -27.002f, 70.174f, -16.178f), reflect(float4(1.2f, 3.6f, -2.8f, 0.3f), float4(1.5f, -1.3f, 3.1f, -0.7f)), 8, false);
            TestUtils.AreEqual(float4(-36.51f, -27.002f, 70.174f, -16.178f), reflect(float4(-1.2f, 3.6f, -2.8f, 0.3f), float4(-1.5f, -1.3f, 3.1f, -0.7f)), 8, false);
            TestUtils.AreEqual(float4(-1.2f, 3.6f, -2.8f, 0.3f), reflect(float4(-1.2f, 3.6f, -2.8f, 0.3f), float4(0.0f, 0.0f, 0.0f, 0.0f)));
            TestUtils.AreEqual(float4(0.0f, 0.0f, 0.0f, 0.0f), reflect(float4(0.0f, 0.0f, 0.0f, 0.0f), float4(-1.5f, -1.3f, 3.1f, -0.7f)), 8, false);
        }


        [TestCompiler]
        public static void reflect_double2()
        {
            TestUtils.AreEqual(double2(9.84, -3.888), reflect(double2(1.2, 3.6), double2(1.5, -1.3)), 8, false);
            TestUtils.AreEqual(double2(-9.84, -3.888), reflect(double2(-1.2, 3.6), double2(-1.5, -1.3)), 8, false);
            TestUtils.AreEqual(double2(-1.2, 3.6), reflect(double2(-1.2, 3.6), double2(0.0, 0.0)));
            TestUtils.AreEqual(double2(0.0, 0.0), reflect(double2(0.0, 0.0), double2(-1.5, -1.3)), 8, false);
        }

        [TestCompiler]
        public static void reflect_double3()
        {
            TestUtils.AreEqual(double3(35.88, -26.456, 68.872), reflect(double3(1.2, 3.6, -2.8), double3(1.5, -1.3, 3.1)), 8, false);
            TestUtils.AreEqual(double3(-35.88, -26.456, 68.872), reflect(double3(-1.2, 3.6, -2.8), double3(-1.5, -1.3, 3.1)), 8, false);
            TestUtils.AreEqual(double3(-1.2, 3.6, -2.8), reflect(double3(-1.2, 3.6, -2.8), double3(0.0, 0.0, 0.0)));
            TestUtils.AreEqual(double3(0.0, 0.0, 0.0), reflect(double3(0.0, 0.0, 0.0), double3(-1.5f, -1.3, 3.1)), 8, false);
        }

        [TestCompiler]
        public static void reflect_double4()
        {
            TestUtils.AreEqual(double4(36.51, -27.002, 70.174, -16.178), reflect(double4(1.2, 3.6, -2.8, 0.3), double4(1.5, -1.3, 3.1, -0.7)), 8, false);
            TestUtils.AreEqual(double4(-36.51, -27.002, 70.174, -16.178), reflect(double4(-1.2, 3.6, -2.8, 0.3), double4(-1.5, -1.3, 3.1, -0.7)), 8, false);
            TestUtils.AreEqual(double4(-1.2, 3.6, -2.8, 0.3), reflect(double4(-1.2, 3.6, -2.8, 0.3), double4(0.0, 0.0, 0.0, 0.0)));
            TestUtils.AreEqual(double4(0.0, 0.0, 0.0, 0.0), reflect(double4(0.0, 0.0, 0.0, 0.0), double4(-1.5, -1.3, 3.1, -0.7)), 8, false);
        }


        [TestCompiler]
        public static void refract_float2()
        {
            TestUtils.AreEqual(float2(-0.3676186f, 0.9299768f), refract(float2(0.316228f, 0.948683f), float2(0.755689f, -0.654931f), 0.5f), 8, false);
            TestUtils.AreEqual(float2(0.4523711f, 0.8918296f), refract(float2(0.316228f, 0.948683f), float2(0.755689f, -0.654931f), 1.05f), 8, false);
            TestUtils.AreEqual(float2(0.0f, 0.0f), refract(float2(0.316228f, 0.948683f), float2(0.755689f, -0.654931f), 1.5f));
        }

        [TestCompiler]
        public static void refract_float3()
        {
            TestUtils.AreEqual(float3(-0.2863437f, 0.8056898f, -0.5185286f), refract(float3(0.288375f, 0.865125f, -0.410365f), float3(0.662147f, -0.573861f, 0.481919f), 0.5f), 8, false);
            TestUtils.AreEqual(float3(0.3743219f, 0.8463902f, -0.3788242f), refract(float3(0.288375f, 0.865125f, -0.410365f), float3(0.662147f, -0.573861f, 0.481919f), 1.05f), 8, false);
            TestUtils.AreEqual(float3(0.0f, 0.0f, 0.0f), refract(float3(0.288375f, 0.865125f, -0.410365f), float3(0.662147f, -0.573861f, 0.481919f), 1.5f));
        }

        [TestCompiler]
        public static void refract_float4()
        {
            TestUtils.AreEqual(float4(-0.302029191645545f, 0.799522577847971f, -0.518952508802814f, -0.015196476378571f), refract(float4(0.278154f, 0.834461f, -0.39582f, -0.26388f), float4(0.652208f, -0.565247f, 0.474685f, -0.1726139f), 0.5f), 16, false);
            TestUtils.AreEqual(float4(0.378159678850401f, 0.801565792862319f, -0.352947832589293f, -0.299860642333894f), refract(float4(0.278154f, 0.834461f, -0.39582f, -0.26388f), float4(0.652208f, -0.565247f, 0.474685f, -0.172613f), 1.05f), 16, false);
            TestUtils.AreEqual(float4(0.0f, 0.0f, 0.0f, 0.0f), refract(float4(0.278154f, 0.834461f, -0.39582f, -0.26388f), float4(0.652208f, -0.565247f, 0.474685f, -0.172613f), 1.5f));
        }


        [TestCompiler]
        public static void refract_double2()
        {
            TestUtils.AreEqual(double2(-0.367618540673032, 0.929976739623085), refract(double2(0.316228, 0.948683), double2(0.755689, -0.654931), 0.5), 8, false);
            TestUtils.AreEqual(double2(0.452371226326029, 0.891829482258995), refract(double2(0.316228, 0.948683), double2(0.755689, -0.654931), 1.05), 8, false);
            TestUtils.AreEqual(double2(0.0, 0.0), refract(double2(0.316228, 0.948683), double2(0.755689, -0.654931), 1.5));
        }

        [TestCompiler]
        public static void refract_double3()
        {
            TestUtils.AreEqual(double3(-0.286343746291412, 0.805689753507206, -0.518528611485079), refract(double3(0.288375, 0.865125, -0.410365), double3(0.662147, -0.573861, 0.481919), 0.5), 8, false);
            TestUtils.AreEqual(double3(0.374321889019825, 0.846390167376268, -0.378824161567529), refract(double3(0.288375, 0.865125, -0.410365), double3(0.662147, -0.573861, 0.481919), 1.05), 8, false);
            TestUtils.AreEqual(double3(0.0, 0.0, 0.0), refract(double3(0.288375, 0.865125, -0.410365), double3(0.662147, -0.573861, 0.481919), 1.5));
        }

        [TestCompiler]
        public static void refract_double4()
        {
            TestUtils.AreEqual(double4(-0.302029191645545, 0.799522577847971, -0.518952508802814, -0.015196476378571), refract(double4(0.278154, 0.834461, -0.39582, -0.26388), double4(0.652208, -0.565247, 0.474685, -0.1726139), 0.5), 16, false);
            TestUtils.AreEqual(double4(0.378159678850401, 0.801565792862319, -0.352947832589293, -0.299860642333894), refract(double4(0.278154, 0.834461, -0.39582, -0.26388), double4(0.652208, -0.565247, 0.474685, -0.172613), 1.05), 16, false);
            TestUtils.AreEqual(double4(0.0, 0.0, 0.0, 0.0), refract(double4(0.278154, 0.834461, -0.39582, -0.26388), double4(0.652208, -0.565247, 0.474685, -0.172613), 1.5));
        }

        [TestCompiler]
        public static void sincos_float()
        {
            float s, c;

            sincos(-1000000f, out s, out c);
            TestUtils.AreEqual(0.3499935f, s, 1, false);
            TestUtils.AreEqual(0.936752141f, c, 8, false);

            sincos(-1.2f, out s, out c);
            TestUtils.AreEqual(-0.9320391f, s, 1, false);
            TestUtils.AreEqual(0.362357765f, c, 8, false);

            sincos(0f, out s, out c);
            TestUtils.AreEqual(0f, s, 1, false);
            TestUtils.AreEqual(1f, c, 8, false);

            sincos(1.2f, out s, out c);
            TestUtils.AreEqual(0.9320391f, s, 1, false);
            TestUtils.AreEqual(0.362357765f, c, 8, false);

            sincos(1000000f, out s, out c);
            TestUtils.AreEqual(-0.3499935f, s, 1, false);
            TestUtils.AreEqual(0.936752141f, c, 8, false);

            sincos(float.NegativeInfinity, out s, out c);
            TestUtils.AreEqual(float.NaN, s, 1, false);
            TestUtils.AreEqual(float.NaN, c, 8, false);

            sincos(float.NaN, out s, out c);
            TestUtils.AreEqual(float.NaN, s, 1, false);
            TestUtils.AreEqual(float.NaN, c, 8, false);

            sincos(float.PositiveInfinity, out s, out c);
            TestUtils.AreEqual(float.NaN, s, 1, false);
            TestUtils.AreEqual(float.NaN, c, 8, false);
        }


        [TestCompiler]
        public static void sincos_float2()
        {
            float2 s, c;

            sincos(float2(-1000000f, -1.2f), out s, out c);
            TestUtils.AreEqual(float2(0.3499935f, -0.9320391f), s, 1, false);
            TestUtils.AreEqual(float2(0.936752141f, 0.362357765f), c, 8, false);

            sincos(float2(0f, 1.2f), out s, out c);
            TestUtils.AreEqual(float2(0f, 0.9320391f), s, 1, false);
            TestUtils.AreEqual(float2(1f, 0.362357765f), c, 8, false);

            sincos(float2(1000000f, float.NegativeInfinity), out s, out c);
            TestUtils.AreEqual(float2(-0.3499935f, float.NaN), s, 1, false);
            TestUtils.AreEqual(float2(0.936752141f, float.NaN), c, 8, false);

            sincos(float2(float.NaN, float.PositiveInfinity), out s, out c);
            TestUtils.AreEqual(float2(float.NaN, float.NaN), s, 1, false);
            TestUtils.AreEqual(float2(float.NaN, float.NaN), c, 8, false);
        }

        [TestCompiler]
        public static void sincos_float3()
        {
            float3 s, c;

            sincos(float3(-1000000f, -1.2f, 0f), out s, out c);
            TestUtils.AreEqual(float3(0.3499935f, -0.9320391f, 0f), s, 1, false);
            TestUtils.AreEqual(float3(0.936752141f, 0.362357765f, 1f), c, 8, false);

            sincos(float3(1.2f, 1000000f, float.NegativeInfinity), out s, out c);
            TestUtils.AreEqual(float3(0.9320391f, -0.3499935f, float.NaN), s, 1, false);
            TestUtils.AreEqual(float3(0.362357765f, 0.936752141f, float.NaN), c, 8, false);

            sincos(float3(float.NaN, float.PositiveInfinity, float.PositiveInfinity), out s, out c);
            TestUtils.AreEqual(float3(float.NaN, float.NaN, float.NaN), s, 1, false);
            TestUtils.AreEqual(float3(float.NaN, float.NaN, float.NaN), c, 8, false);
        }

        [TestCompiler]
        public static void sincos_float4()
        {
            float4 s, c;

            sincos(float4(-1000000f, -1.2f, 0f, 1.2f), out s, out c);
            TestUtils.AreEqual(float4(0.3499935f, -0.9320391f, 0f, 0.9320391f), s, 1, false);
            TestUtils.AreEqual(float4(0.936752141f, 0.362357765f, 1f, 0.362357765f), c, 8, false);

            sincos(float4(1000000f, float.NegativeInfinity, float.NaN, float.PositiveInfinity), out s, out c);
            TestUtils.AreEqual(float4(-0.3499935f, float.NaN, float.NaN, float.NaN), s, 1, false);
            TestUtils.AreEqual(float4(0.936752141f, float.NaN, float.NaN, float.NaN), c, 8, false);
        }

        [TestCompiler]
        public static void sincos_double()
        {
            double s, c;
            sincos(-1000000.0, out s, out c);
            TestUtils.AreEqual(0.34999350217129294, s, 32, false);
            TestUtils.AreEqual(0.93675212753314474, c, 32, false);

            sincos(-1.2, out s, out c);
            TestUtils.AreEqual(-0.9320390859672264, s, 32, false);
            TestUtils.AreEqual(0.36235775447667357, c, 32, false);

            sincos(0.0, out s, out c);
            TestUtils.AreEqual(0.0, s, 32, false);
            TestUtils.AreEqual(1.0, c, 32, false);

            sincos(1.2, out s, out c);
            TestUtils.AreEqual(0.9320390859672264, s, 32, false);
            TestUtils.AreEqual(0.36235775447667357, c, 32, false);

            sincos(1000000.0, out s, out c);
            TestUtils.AreEqual(-0.34999350217129294, s, 32, false);
            TestUtils.AreEqual(0.93675212753314474, c, 32, false);

            sincos(double.NegativeInfinity, out s, out c);
            TestUtils.AreEqual(double.NaN, s, 32, false);
            TestUtils.AreEqual(double.NaN, c, 32, false);

            sincos(double.NaN, out s, out c);
            TestUtils.AreEqual(double.NaN, s, 32, false);
            TestUtils.AreEqual(double.NaN, c, 32, false);

            sincos(double.NaN, out s, out c);
            TestUtils.AreEqual(double.NaN, s, 32, false);
            TestUtils.AreEqual(double.NaN, c, 32, false);
        }

        [TestCompiler]
        public static void sincos_double2()
        {
            double2 s, c;
            sincos(double2(-1000000.0, -1.2), out s, out c);
            TestUtils.AreEqual(double2(0.34999350217129294, -0.9320390859672264), s, 32, false);
            TestUtils.AreEqual(double2(0.93675212753314474, 0.36235775447667357), c, 32, false);

            sincos(double2(0.0, 1.2), out s, out c);
            TestUtils.AreEqual(double2(0.0, 0.9320390859672264), s, 32, false);
            TestUtils.AreEqual(double2(1.0, 0.36235775447667357), c, 32, false);

            sincos(double2(1000000.0, double.NegativeInfinity), out s, out c);
            TestUtils.AreEqual(double2(-0.34999350217129294, double.NaN), s, 32, false);
            TestUtils.AreEqual(double2(0.93675212753314474, double.NaN), c, 32, false);

            sincos(double2(double.NaN, double.PositiveInfinity), out s, out c);
            TestUtils.AreEqual(double2(double.NaN, double.NaN), s, 32, false);
            TestUtils.AreEqual(double2(double.NaN, double.NaN), c, 32, false);
        }

        [TestCompiler]
        public static void sincos_double3()
        {
            double3 s, c;

            sincos(double3(-1000000.0, -1.2, 0.0), out s, out c);
            TestUtils.AreEqual(double3(0.34999350217129294, -0.9320390859672264, 0.0), s, 32, false);
            TestUtils.AreEqual(double3(0.93675212753314474, 0.36235775447667357, 1.0), c, 32, false);

            sincos(double3(1.2, 1000000.0, double.NegativeInfinity), out s, out c);
            TestUtils.AreEqual(double3(0.9320390859672264, -0.34999350217129294, double.NaN), s, 32, false);
            TestUtils.AreEqual(double3(0.36235775447667357, 0.93675212753314474, double.NaN), c, 32, false);

            sincos(double3(double.NaN, double.PositiveInfinity, double.PositiveInfinity), out s, out c);
            TestUtils.AreEqual(double3(double.NaN, double.NaN, double.NaN), s, 32, false);
            TestUtils.AreEqual(double3(double.NaN, double.NaN, double.NaN), c, 32, false);
        }

        [TestCompiler]
        public static void sincos_double4()
        {
            double4 s, c;

            sincos(double4(-1000000.0, -1.2, 0.0, 1.2), out s, out c);
            TestUtils.AreEqual(double4(0.34999350217129294, -0.9320390859672264, 0.0, 0.9320390859672264), s, 32, false);
            TestUtils.AreEqual(double4(0.93675212753314474, 0.36235775447667357, 1.0, 0.36235775447667357), c, 32, false);

            sincos(double4(1000000.0, double.NegativeInfinity, double.NaN, double.PositiveInfinity), out s, out c);
            TestUtils.AreEqual(double4(-0.34999350217129294, double.NaN, double.NaN, double.NaN), s, 32, false);
            TestUtils.AreEqual(double4(0.93675212753314474, double.NaN, double.NaN, double.NaN), c, 32, false);
        }

        [TestCompiler]
        public static void select_int()
        {
            TestUtils.AreEqual(-123456789, select(-123456789, 987654321, false));
            TestUtils.AreEqual(987654321, select(-123456789, 987654321, true));
        }

        [TestCompiler]
        public static void select_int2()
        {
            TestUtils.AreEqual(int2(-123456789, -123456790), select(int2(-123456789, -123456790), int2(987654321, 987654322), false));
            TestUtils.AreEqual(int2(987654321, 987654322), select(int2(-123456789, -123456790), int2(987654321, 987654322), true));

            TestUtils.AreEqual(int2(-123456789, -123456790), select(int2(-123456789, -123456790), int2(987654321, 987654322), bool2(false, false)));
            TestUtils.AreEqual(int2(-123456789, 987654322), select(int2(-123456789, -123456790), int2(987654321, 987654322), bool2(false, true)));
            TestUtils.AreEqual(int2(987654321, -123456790), select(int2(-123456789, -123456790), int2(987654321, 987654322), bool2(true, false)));
            TestUtils.AreEqual(int2(987654321, 987654322), select(int2(-123456789, -123456790), int2(987654321, 987654322), bool2(true, true)));
        }

        [TestCompiler]
        public static void select_int3()
        {
            TestUtils.AreEqual(int3(-123456789, -123456790, -123456791), select(int3(-123456789, -123456790, -123456791), int3(987654321, 987654322, 987654323), false));
            TestUtils.AreEqual(int3(987654321, 987654322, 987654323), select(int3(-123456789, -123456790, -123456791), int3(987654321, 987654322, 987654323), true));

            TestUtils.AreEqual(int3(-123456789, -123456790, -123456791), select(int3(-123456789, -123456790, -123456791), int3(987654321, 987654322, 987654323), bool3(false, false, false)));
            TestUtils.AreEqual(int3(-123456789, -123456790, 987654323), select(int3(-123456789, -123456790, -123456791), int3(987654321, 987654322, 987654323), bool3(false, false, true)));
            TestUtils.AreEqual(int3(-123456789, 987654322, -123456791), select(int3(-123456789, -123456790, -123456791), int3(987654321, 987654322, 987654323), bool3(false, true, false)));
            TestUtils.AreEqual(int3(-123456789, 987654322, 987654323), select(int3(-123456789, -123456790, -123456791), int3(987654321, 987654322, 987654323), bool3(false, true, true)));

            TestUtils.AreEqual(int3(987654321, -123456790, -123456791), select(int3(-123456789, -123456790, -123456791), int3(987654321, 987654322, 987654323), bool3(true, false, false)));
            TestUtils.AreEqual(int3(987654321, -123456790, 987654323), select(int3(-123456789, -123456790, -123456791), int3(987654321, 987654322, 987654323), bool3(true, false, true)));
            TestUtils.AreEqual(int3(987654321, 987654322, -123456791), select(int3(-123456789, -123456790, -123456791), int3(987654321, 987654322, 987654323), bool3(true, true, false)));
            TestUtils.AreEqual(int3(987654321, 987654322, 987654323), select(int3(-123456789, -123456790, -123456791), int3(987654321, 987654322, 987654323), bool3(true, true, true)));
        }

        [TestCompiler]
        public static void select_int4()
        {
            TestUtils.AreEqual(int4(-123456789, -123456790, -123456791, -123456792), select(int4(-123456789, -123456790, -123456791, -123456792), int4(987654321, 987654322, 987654323, 987654324), false));
            TestUtils.AreEqual(int4(987654321, 987654322, 987654323, 987654324), select(int4(-123456789, -123456790, -123456791, -123456792), int4(987654321, 987654322, 987654323, 987654324), true));

            TestUtils.AreEqual(int4(-123456789, -123456790, -123456791, -123456792), select(int4(-123456789, -123456790, -123456791, -123456792), int4(987654321, 987654322, 987654323, 987654324), bool4(false, false, false, false)));
            TestUtils.AreEqual(int4(-123456789, -123456790, -123456791, 987654324), select(int4(-123456789, -123456790, -123456791, -123456792), int4(987654321, 987654322, 987654323, 987654324), bool4(false, false, false, true)));
            TestUtils.AreEqual(int4(-123456789, -123456790, 987654323, -123456792), select(int4(-123456789, -123456790, -123456791, -123456792), int4(987654321, 987654322, 987654323, 987654324), bool4(false, false, true, false)));
            TestUtils.AreEqual(int4(-123456789, -123456790, 987654323, 987654324), select(int4(-123456789, -123456790, -123456791, -123456792), int4(987654321, 987654322, 987654323, 987654324), bool4(false, false, true, true)));

            TestUtils.AreEqual(int4(-123456789, 987654322, -123456791, -123456792), select(int4(-123456789, -123456790, -123456791, -123456792), int4(987654321, 987654322, 987654323, 987654324), bool4(false, true, false, false)));
            TestUtils.AreEqual(int4(-123456789, 987654322, -123456791, 987654324), select(int4(-123456789, -123456790, -123456791, -123456792), int4(987654321, 987654322, 987654323, 987654324), bool4(false, true, false, true)));
            TestUtils.AreEqual(int4(-123456789, 987654322, 987654323, -123456792), select(int4(-123456789, -123456790, -123456791, -123456792), int4(987654321, 987654322, 987654323, 987654324), bool4(false, true, true, false)));
            TestUtils.AreEqual(int4(-123456789, 987654322, 987654323, 987654324), select(int4(-123456789, -123456790, -123456791, -123456792), int4(987654321, 987654322, 987654323, 987654324), bool4(false, true, true, true)));

            TestUtils.AreEqual(int4(987654321, -123456790, -123456791, -123456792), select(int4(-123456789, -123456790, -123456791, -123456792), int4(987654321, 987654322, 987654323, 987654324), bool4(true, false, false, false)));
            TestUtils.AreEqual(int4(987654321, -123456790, -123456791, 987654324), select(int4(-123456789, -123456790, -123456791, -123456792), int4(987654321, 987654322, 987654323, 987654324), bool4(true, false, false, true)));
            TestUtils.AreEqual(int4(987654321, -123456790, 987654323, -123456792), select(int4(-123456789, -123456790, -123456791, -123456792), int4(987654321, 987654322, 987654323, 987654324), bool4(true, false, true, false)));
            TestUtils.AreEqual(int4(987654321, -123456790, 987654323, 987654324), select(int4(-123456789, -123456790, -123456791, -123456792), int4(987654321, 987654322, 987654323, 987654324), bool4(true, false, true, true)));

            TestUtils.AreEqual(int4(987654321, 987654322, -123456791, -123456792), select(int4(-123456789, -123456790, -123456791, -123456792), int4(987654321, 987654322, 987654323, 987654324), bool4(true, true, false, false)));
            TestUtils.AreEqual(int4(987654321, 987654322, -123456791, 987654324), select(int4(-123456789, -123456790, -123456791, -123456792), int4(987654321, 987654322, 987654323, 987654324), bool4(true, true, false, true)));
            TestUtils.AreEqual(int4(987654321, 987654322, 987654323, -123456792), select(int4(-123456789, -123456790, -123456791, -123456792), int4(987654321, 987654322, 987654323, 987654324), bool4(true, true, true, false)));
            TestUtils.AreEqual(int4(987654321, 987654322, 987654323, 987654324), select(int4(-123456789, -123456790, -123456791, -123456792), int4(987654321, 987654322, 987654323, 987654324), bool4(true, true, true, true)));
        }

        [TestCompiler]
        public static void select_uint()
        {
            TestUtils.AreEqual(123456789u, select(123456789u, 987654321u, false));
            TestUtils.AreEqual(987654321u, select(123456789u, 987654321u, true));
        }

        [TestCompiler]
        public static void select_uint2()
        {
            TestUtils.AreEqual(uint2(123456789u, 123456790u), select(uint2(123456789u, 123456790u), uint2(987654321u, 987654322u), false));
            TestUtils.AreEqual(uint2(987654321u, 987654322), select(uint2(123456789u, 123456790u), uint2(987654321u, 987654322u), true));

            TestUtils.AreEqual(uint2(123456789u, 123456790u), select(uint2(123456789u, 123456790u), uint2(987654321u, 987654322u), bool2(false, false)));
            TestUtils.AreEqual(uint2(123456789u, 987654322u), select(uint2(123456789u, 123456790u), uint2(987654321u, 987654322u), bool2(false, true)));
            TestUtils.AreEqual(uint2(987654321u, 123456790u), select(uint2(123456789u, 123456790u), uint2(987654321u, 987654322u), bool2(true, false)));
            TestUtils.AreEqual(uint2(987654321u, 987654322u), select(uint2(123456789u, 123456790u), uint2(987654321u, 987654322u), bool2(true, true)));
        }

        [TestCompiler]
        public static void select_uint3()
        {
            TestUtils.AreEqual(uint3(123456789u, 123456790u, 123456791u), select(uint3(123456789u, 123456790u, 123456791u), uint3(987654321u, 987654322u, 987654323u), false));
            TestUtils.AreEqual(uint3(987654321u, 987654322u, 987654323), select(uint3(123456789u, 123456790u, 123456791u), uint3(987654321u, 987654322u, 987654323u), true));

            TestUtils.AreEqual(uint3(123456789u, 123456790u, 123456791u), select(uint3(123456789u, 123456790u, 123456791u), uint3(987654321u, 987654322u, 987654323u), bool3(false, false, false)));
            TestUtils.AreEqual(uint3(123456789u, 123456790u, 987654323u), select(uint3(123456789u, 123456790u, 123456791u), uint3(987654321u, 987654322u, 987654323u), bool3(false, false, true)));
            TestUtils.AreEqual(uint3(123456789u, 987654322u, 123456791u), select(uint3(123456789u, 123456790u, 123456791u), uint3(987654321u, 987654322u, 987654323u), bool3(false, true, false)));
            TestUtils.AreEqual(uint3(123456789u, 987654322u, 987654323u), select(uint3(123456789u, 123456790u, 123456791u), uint3(987654321u, 987654322u, 987654323u), bool3(false, true, true)));

            TestUtils.AreEqual(uint3(987654321u, 123456790u, 123456791u), select(uint3(123456789u, 123456790u, 123456791u), uint3(987654321u, 987654322u, 987654323u), bool3(true, false, false)));
            TestUtils.AreEqual(uint3(987654321u, 123456790u, 987654323u), select(uint3(123456789u, 123456790u, 123456791u), uint3(987654321u, 987654322u, 987654323u), bool3(true, false, true)));
            TestUtils.AreEqual(uint3(987654321u, 987654322u, 123456791u), select(uint3(123456789u, 123456790u, 123456791u), uint3(987654321u, 987654322u, 987654323u), bool3(true, true, false)));
            TestUtils.AreEqual(uint3(987654321u, 987654322u, 987654323u), select(uint3(123456789u, 123456790u, 123456791u), uint3(987654321u, 987654322u, 987654323u), bool3(true, true, true)));
        }

        [TestCompiler]
        public static void select_uint4()
        {
            TestUtils.AreEqual(uint4(123456789u, 123456790u, 123456791u, 123456792u), select(uint4(123456789u, 123456790u, 123456791u, 123456792u), uint4(987654321u, 987654322u, 987654323u, 987654324u), false));
            TestUtils.AreEqual(uint4(987654321u, 987654322u, 987654323u, 987654324u), select(uint4(123456789u, 123456790u, 123456791u, 123456792u), uint4(987654321u, 987654322u, 987654323u, 987654324u), true));

            TestUtils.AreEqual(uint4(123456789u, 123456790u, 123456791u, 123456792u), select(uint4(123456789u, 123456790u, 123456791u, 123456792u), uint4(987654321u, 987654322u, 987654323u, 987654324u), bool4(false, false, false, false)));
            TestUtils.AreEqual(uint4(123456789u, 123456790u, 123456791u, 987654324u), select(uint4(123456789u, 123456790u, 123456791u, 123456792u), uint4(987654321u, 987654322u, 987654323u, 987654324u), bool4(false, false, false, true)));
            TestUtils.AreEqual(uint4(123456789u, 123456790u, 987654323u, 123456792u), select(uint4(123456789u, 123456790u, 123456791u, 123456792u), uint4(987654321u, 987654322u, 987654323u, 987654324u), bool4(false, false, true, false)));
            TestUtils.AreEqual(uint4(123456789u, 123456790u, 987654323u, 987654324u), select(uint4(123456789u, 123456790u, 123456791u, 123456792u), uint4(987654321u, 987654322u, 987654323u, 987654324u), bool4(false, false, true, true)));

            TestUtils.AreEqual(uint4(123456789u, 987654322u, 123456791u, 123456792u), select(uint4(123456789u, 123456790u, 123456791u, 123456792u), uint4(987654321u, 987654322u, 987654323u, 987654324u), bool4(false, true, false, false)));
            TestUtils.AreEqual(uint4(123456789u, 987654322u, 123456791u, 987654324u), select(uint4(123456789u, 123456790u, 123456791u, 123456792u), uint4(987654321u, 987654322u, 987654323u, 987654324u), bool4(false, true, false, true)));
            TestUtils.AreEqual(uint4(123456789u, 987654322u, 987654323u, 123456792u), select(uint4(123456789u, 123456790u, 123456791u, 123456792u), uint4(987654321u, 987654322u, 987654323u, 987654324u), bool4(false, true, true, false)));
            TestUtils.AreEqual(uint4(123456789u, 987654322u, 987654323u, 987654324u), select(uint4(123456789u, 123456790u, 123456791u, 123456792u), uint4(987654321u, 987654322u, 987654323u, 987654324u), bool4(false, true, true, true)));

            TestUtils.AreEqual(uint4(987654321u, 123456790u, 123456791u, 123456792u), select(uint4(123456789u, 123456790u, 123456791u, 123456792u), uint4(987654321u, 987654322u, 987654323u, 987654324u), bool4(true, false, false, false)));
            TestUtils.AreEqual(uint4(987654321u, 123456790u, 123456791u, 987654324u), select(uint4(123456789u, 123456790u, 123456791u, 123456792u), uint4(987654321u, 987654322u, 987654323u, 987654324u), bool4(true, false, false, true)));
            TestUtils.AreEqual(uint4(987654321u, 123456790u, 987654323u, 123456792u), select(uint4(123456789u, 123456790u, 123456791u, 123456792u), uint4(987654321u, 987654322u, 987654323u, 987654324u), bool4(true, false, true, false)));
            TestUtils.AreEqual(uint4(987654321u, 123456790u, 987654323u, 987654324u), select(uint4(123456789u, 123456790u, 123456791u, 123456792u), uint4(987654321u, 987654322u, 987654323u, 987654324u), bool4(true, false, true, true)));

            TestUtils.AreEqual(uint4(987654321u, 987654322u, 123456791u, 123456792u), select(uint4(123456789u, 123456790u, 123456791u, 123456792u), uint4(987654321u, 987654322u, 987654323u, 987654324u), bool4(true, true, false, false)));
            TestUtils.AreEqual(uint4(987654321u, 987654322u, 123456791u, 987654324u), select(uint4(123456789u, 123456790u, 123456791u, 123456792u), uint4(987654321u, 987654322u, 987654323u, 987654324u), bool4(true, true, false, true)));
            TestUtils.AreEqual(uint4(987654321u, 987654322u, 987654323u, 123456792u), select(uint4(123456789u, 123456790u, 123456791u, 123456792u), uint4(987654321u, 987654322u, 987654323u, 987654324u), bool4(true, true, true, false)));
            TestUtils.AreEqual(uint4(987654321u, 987654322u, 987654323u, 987654324u), select(uint4(123456789u, 123456790u, 123456791u, 123456792u), uint4(987654321u, 987654322u, 987654323u, 987654324u), bool4(true, true, true, true)));
        }

        [TestCompiler]
        public static void select_long()
        {
            TestUtils.AreEqual(-12345678910111314L, select(-12345678910111314L, 987654321011121314L, false));
            TestUtils.AreEqual(987654321011121314L, select(-12345678910111314L, 987654321011121314L, true));
        }

        [TestCompiler]
        public static void select_ulong()
        {
            TestUtils.AreEqual(12345678910111314UL, select(12345678910111314UL, 987654321011121314UL, false));
            TestUtils.AreEqual(987654321011121314UL, select(12345678910111314UL, 987654321011121314UL, true));
        }


        [TestCompiler]
        public static void select_float()
        {
            TestUtils.AreEqual(-1234.5f, select(-1234.5f, 9876.25f, false));
            TestUtils.AreEqual(9876.25f, select(-1234.5f, 9876.25f, true));
        }

        [TestCompiler]
        public static void select_float2()
        {
            TestUtils.AreEqual(float2(-1234.5f, -1235.5f), select(float2(-1234.5f, -1235.5f), float2(9876.25f, 9877.25f), false));
            TestUtils.AreEqual(float2(9876.25f, 9877.25f), select(float2(-1234.5f, -1235.5f), float2(9876.25f, 9877.25f), true));

            TestUtils.AreEqual(float2(-1234.5f, -1235.5f), select(float2(-1234.5f, -1235.5f), float2(9876.25f, 9877.25f), bool2(false, false)));
            TestUtils.AreEqual(float2(-1234.5f, 9877.25f), select(float2(-1234.5f, -1235.5f), float2(9876.25f, 9877.25f), bool2(false, true)));
            TestUtils.AreEqual(float2(9876.25f, -1235.5f), select(float2(-1234.5f, -1235.5f), float2(9876.25f, 9877.25f), bool2(true, false)));
            TestUtils.AreEqual(float2(9876.25f, 9877.25f), select(float2(-1234.5f, -1235.5f), float2(9876.25f, 9877.25f), bool2(true, true)));
        }

        [TestCompiler]
        public static void select_float3()
        {
            TestUtils.AreEqual(float3(-1234.5f, -1235.5f, -1236.5f), select(float3(-1234.5f, -1235.5f, -1236.5f), float3(9876.25f, 9877.25f, 9878.25f), false));
            TestUtils.AreEqual(float3(9876.25f, 9877.25f, 9878.25f), select(float3(-1234.5f, -1235.5f, -1236.5f), float3(9876.25f, 9877.25f, 9878.25f), true));

            TestUtils.AreEqual(float3(-1234.5f, -1235.5f, -1236.5f), select(float3(-1234.5f, -1235.5f, -1236.5f), float3(9876.25f, 9877.25f, 9878.25f), bool3(false, false, false)));
            TestUtils.AreEqual(float3(-1234.5f, -1235.5f, 9878.25f), select(float3(-1234.5f, -1235.5f, -1236.5f), float3(9876.25f, 9877.25f, 9878.25f), bool3(false, false, true)));
            TestUtils.AreEqual(float3(-1234.5f, 9877.25f, -1236.5f), select(float3(-1234.5f, -1235.5f, -1236.5f), float3(9876.25f, 9877.25f, 9878.25f), bool3(false, true, false)));
            TestUtils.AreEqual(float3(-1234.5f, 9877.25f, 9878.25f), select(float3(-1234.5f, -1235.5f, -1236.5f), float3(9876.25f, 9877.25f, 9878.25f), bool3(false, true, true)));

            TestUtils.AreEqual(float3(9876.25f, -1235.5f, -1236.5f), select(float3(-1234.5f, -1235.5f, -1236.5f), float3(9876.25f, 9877.25f, 9878.25f), bool3(true, false, false)));
            TestUtils.AreEqual(float3(9876.25f, -1235.5f, 9878.25f), select(float3(-1234.5f, -1235.5f, -1236.5f), float3(9876.25f, 9877.25f, 9878.25f), bool3(true, false, true)));
            TestUtils.AreEqual(float3(9876.25f, 9877.25f, -1236.5f), select(float3(-1234.5f, -1235.5f, -1236.5f), float3(9876.25f, 9877.25f, 9878.25f), bool3(true, true, false)));
            TestUtils.AreEqual(float3(9876.25f, 9877.25f, 9878.25f), select(float3(-1234.5f, -1235.5f, -1236.5f), float3(9876.25f, 9877.25f, 9878.25f), bool3(true, true, true)));
        }

        [TestCompiler]
        public static void select_float4()
        {
            TestUtils.AreEqual(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), select(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), false));
            TestUtils.AreEqual(float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), select(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), true));

            TestUtils.AreEqual(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), select(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), bool4(false, false, false, false)));
            TestUtils.AreEqual(float4(-1234.5f, -1235.5f, -1236.5f, 9879.25f), select(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), bool4(false, false, false, true)));
            TestUtils.AreEqual(float4(-1234.5f, -1235.5f, 9878.25f, -1237.5f), select(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), bool4(false, false, true, false)));
            TestUtils.AreEqual(float4(-1234.5f, -1235.5f, 9878.25f, 9879.25f), select(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), bool4(false, false, true, true)));

            TestUtils.AreEqual(float4(-1234.5f, 9877.25f, -1236.5f, -1237.5f), select(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), bool4(false, true, false, false)));
            TestUtils.AreEqual(float4(-1234.5f, 9877.25f, -1236.5f, 9879.25f), select(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), bool4(false, true, false, true)));
            TestUtils.AreEqual(float4(-1234.5f, 9877.25f, 9878.25f, -1237.5f), select(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), bool4(false, true, true, false)));
            TestUtils.AreEqual(float4(-1234.5f, 9877.25f, 9878.25f, 9879.25f), select(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), bool4(false, true, true, true)));

            TestUtils.AreEqual(float4(9876.25f, -1235.5f, -1236.5f, -1237.5f), select(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), bool4(true, false, false, false)));
            TestUtils.AreEqual(float4(9876.25f, -1235.5f, -1236.5f, 9879.25f), select(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), bool4(true, false, false, true)));
            TestUtils.AreEqual(float4(9876.25f, -1235.5f, 9878.25f, -1237.5f), select(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), bool4(true, false, true, false)));
            TestUtils.AreEqual(float4(9876.25f, -1235.5f, 9878.25f, 9879.25f), select(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), bool4(true, false, true, true)));

            TestUtils.AreEqual(float4(9876.25f, 9877.25f, -1236.5f, -1237.5f), select(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), bool4(true, true, false, false)));
            TestUtils.AreEqual(float4(9876.25f, 9877.25f, -1236.5f, 9879.25f), select(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), bool4(true, true, false, true)));
            TestUtils.AreEqual(float4(9876.25f, 9877.25f, 9878.25f, -1237.5f), select(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), bool4(true, true, true, false)));
            TestUtils.AreEqual(float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), select(float4(-1234.5f, -1235.5f, -1236.5f, -1237.5f), float4(9876.25f, 9877.25f, 9878.25f, 9879.25f), bool4(true, true, true, true)));
        }


        [TestCompiler]
        public static void select_double()
        {
            TestUtils.AreEqual(-1234.5, select(-1234.5, 9876.25, false));
            TestUtils.AreEqual(9876.25, select(-1234.5, 9876.25, true));
        }

        [TestCompiler]
        public static void select_double2()
        {
            TestUtils.AreEqual(double2(-1234.5, -1235.5), select(double2(-1234.5, -1235.5), double2(9876.25, 9877.25), false));
            TestUtils.AreEqual(double2(9876.25, 9877.25), select(double2(-1234.5, -1235.5), double2(9876.25, 9877.25), true));

            TestUtils.AreEqual(double2(-1234.5, -1235.5), select(double2(-1234.5, -1235.5), double2(9876.25, 9877.25), bool2(false, false)));
            TestUtils.AreEqual(double2(-1234.5, 9877.25), select(double2(-1234.5, -1235.5), double2(9876.25, 9877.25), bool2(false, true)));
            TestUtils.AreEqual(double2(9876.25, -1235.5), select(double2(-1234.5, -1235.5), double2(9876.25, 9877.25), bool2(true, false)));
            TestUtils.AreEqual(double2(9876.25, 9877.25), select(double2(-1234.5, -1235.5), double2(9876.25, 9877.25), bool2(true, true)));
        }

        [TestCompiler]
        public static void select_double3()
        {
            TestUtils.AreEqual(double3(-1234.5, -1235.5, -1236.5), select(double3(-1234.5, -1235.5, -1236.5), double3(9876.25, 9877.25, 9878.25), false));
            TestUtils.AreEqual(double3(9876.25, 9877.25, 9878.25), select(double3(-1234.5, -1235.5, -1236.5), double3(9876.25, 9877.25, 9878.25), true));

            TestUtils.AreEqual(double3(-1234.5, -1235.5, -1236.5), select(double3(-1234.5, -1235.5, -1236.5), double3(9876.25, 9877.25, 9878.25), bool3(false, false, false)));
            TestUtils.AreEqual(double3(-1234.5, -1235.5, 9878.25), select(double3(-1234.5, -1235.5, -1236.5), double3(9876.25, 9877.25, 9878.25), bool3(false, false, true)));
            TestUtils.AreEqual(double3(-1234.5, 9877.25, -1236.5), select(double3(-1234.5, -1235.5, -1236.5), double3(9876.25, 9877.25, 9878.25), bool3(false, true, false)));
            TestUtils.AreEqual(double3(-1234.5, 9877.25, 9878.25), select(double3(-1234.5, -1235.5, -1236.5), double3(9876.25, 9877.25, 9878.25), bool3(false, true, true)));

            TestUtils.AreEqual(double3(9876.25, -1235.5, -1236.5), select(double3(-1234.5, -1235.5, -1236.5), double3(9876.25, 9877.25, 9878.25), bool3(true, false, false)));
            TestUtils.AreEqual(double3(9876.25, -1235.5, 9878.25), select(double3(-1234.5, -1235.5, -1236.5), double3(9876.25, 9877.25, 9878.25), bool3(true, false, true)));
            TestUtils.AreEqual(double3(9876.25, 9877.25, -1236.5), select(double3(-1234.5, -1235.5, -1236.5), double3(9876.25, 9877.25, 9878.25), bool3(true, true, false)));
            TestUtils.AreEqual(double3(9876.25, 9877.25, 9878.25), select(double3(-1234.5, -1235.5, -1236.5), double3(9876.25, 9877.25, 9878.25), bool3(true, true, true)));
        }

        [TestCompiler]
        public static void select_double4()
        {
            TestUtils.AreEqual(double4(-1234.5, -1235.5, -1236.5, -1237.5), select(double4(-1234.5, -1235.5, -1236.5, -1237.5), double4(9876.25, 9877.25, 9878.25, 9879.25), false));
            TestUtils.AreEqual(double4(9876.25, 9877.25, 9878.25, 9879.25), select(double4(-1234.5, -1235.5, -1236.5, -1237.5), double4(9876.25, 9877.25, 9878.25, 9879.25), true));

            TestUtils.AreEqual(double4(-1234.5, -1235.5, -1236.5, -1237.5), select(double4(-1234.5, -1235.5, -1236.5, -1237.5), double4(9876.25, 9877.25, 9878.25, 9879.25), bool4(false, false, false, false)));
            TestUtils.AreEqual(double4(-1234.5, -1235.5, -1236.5, 9879.25), select(double4(-1234.5, -1235.5, -1236.5, -1237.5), double4(9876.25, 9877.25, 9878.25, 9879.25), bool4(false, false, false, true)));
            TestUtils.AreEqual(double4(-1234.5, -1235.5, 9878.25, -1237.5), select(double4(-1234.5, -1235.5, -1236.5, -1237.5), double4(9876.25, 9877.25, 9878.25, 9879.25), bool4(false, false, true, false)));
            TestUtils.AreEqual(double4(-1234.5, -1235.5, 9878.25, 9879.25), select(double4(-1234.5, -1235.5, -1236.5, -1237.5), double4(9876.25, 9877.25, 9878.25, 9879.25), bool4(false, false, true, true)));

            TestUtils.AreEqual(double4(-1234.5, 9877.25, -1236.5, -1237.5), select(double4(-1234.5, -1235.5, -1236.5, -1237.5), double4(9876.25, 9877.25, 9878.25, 9879.25), bool4(false, true, false, false)));
            TestUtils.AreEqual(double4(-1234.5, 9877.25, -1236.5, 9879.25), select(double4(-1234.5, -1235.5, -1236.5, -1237.5), double4(9876.25, 9877.25, 9878.25, 9879.25), bool4(false, true, false, true)));
            TestUtils.AreEqual(double4(-1234.5, 9877.25, 9878.25, -1237.5), select(double4(-1234.5, -1235.5, -1236.5, -1237.5), double4(9876.25, 9877.25, 9878.25, 9879.25), bool4(false, true, true, false)));
            TestUtils.AreEqual(double4(-1234.5, 9877.25, 9878.25, 9879.25), select(double4(-1234.5, -1235.5, -1236.5, -1237.5), double4(9876.25, 9877.25, 9878.25, 9879.25), bool4(false, true, true, true)));

            TestUtils.AreEqual(double4(9876.25, -1235.5, -1236.5, -1237.5), select(double4(-1234.5, -1235.5, -1236.5, -1237.5), double4(9876.25, 9877.25, 9878.25, 9879.25), bool4(true, false, false, false)));
            TestUtils.AreEqual(double4(9876.25, -1235.5, -1236.5, 9879.25), select(double4(-1234.5, -1235.5, -1236.5, -1237.5), double4(9876.25, 9877.25, 9878.25, 9879.25), bool4(true, false, false, true)));
            TestUtils.AreEqual(double4(9876.25, -1235.5, 9878.25, -1237.5), select(double4(-1234.5, -1235.5, -1236.5, -1237.5), double4(9876.25, 9877.25, 9878.25, 9879.25), bool4(true, false, true, false)));
            TestUtils.AreEqual(double4(9876.25, -1235.5, 9878.25, 9879.25), select(double4(-1234.5, -1235.5, -1236.5, -1237.5), double4(9876.25, 9877.25, 9878.25, 9879.25), bool4(true, false, true, true)));

            TestUtils.AreEqual(double4(9876.25, 9877.25, -1236.5, -1237.5), select(double4(-1234.5, -1235.5, -1236.5, -1237.5), double4(9876.25, 9877.25, 9878.25, 9879.25), bool4(true, true, false, false)));
            TestUtils.AreEqual(double4(9876.25, 9877.25, -1236.5, 9879.25), select(double4(-1234.5, -1235.5, -1236.5, -1237.5), double4(9876.25, 9877.25, 9878.25, 9879.25), bool4(true, true, false, true)));
            TestUtils.AreEqual(double4(9876.25, 9877.25, 9878.25, -1237.5), select(double4(-1234.5, -1235.5, -1236.5, -1237.5), double4(9876.25, 9877.25, 9878.25, 9879.25), bool4(true, true, true, false)));
            TestUtils.AreEqual(double4(9876.25, 9877.25, 9878.25, 9879.25), select(double4(-1234.5, -1235.5, -1236.5, -1237.5), double4(9876.25, 9877.25, 9878.25, 9879.25), bool4(true, true, true, true)));
        }


        [TestCompiler]
        public static void dot_int()
        {
            TestUtils.AreEqual(133, dot(7, 19));
            TestUtils.AreEqual(-133, dot(-7, 19));
            TestUtils.AreEqual(-1773654624, dot(61031, 41312));
            TestUtils.AreEqual(667585376, dot(61031, 81312));
        }

        [TestCompiler]
        public static void dot_int2()
        {
            TestUtils.AreEqual(322, dot(int2(7, 9), int2(19, 21)));
            TestUtils.AreEqual(-322, dot(int2(-7, 9), int2(19, -21)));
            TestUtils.AreEqual(-1707074016, dot(int2(61031, 12534), int2(41312, 5312)));
            TestUtils.AreEqual(1840235232, dot(int2(61031, -12534), int2(-41312, -5312)));
        }

        [TestCompiler]
        public static void dot_int3()
        {
            TestUtils.AreEqual(582, dot(int3(7, 9, 13), int3(19, 21, 20)));
            TestUtils.AreEqual(-582, dot(int3(-7, 9, 13), int3(19, -21, -20)));
            TestUtils.AreEqual(-1503299063, dot(int3(61031, 12534, 9211), int3(41312, 5312, 22123)));
            TestUtils.AreEqual(1636460279, dot(int3(61031, -12534, 9211), int3(-41312, -5312, -22123)));
        }

        [TestCompiler]
        public static void dot_int4()
        {
            TestUtils.AreEqual(990, dot(int4(7, 9, 13, 17), int4(19, 21, 20, 24)));
            TestUtils.AreEqual(-990, dot(int4(-7, 9, 13, -17), int4(19, -21, -20, 24)));
            TestUtils.AreEqual(663641543, dot(int4(61031, 12534, 9211, 33122), int4(41312, 5312, 22123, 65423)));
            TestUtils.AreEqual(-491566411, dot(int4(61031, -12534, 9211, 33122), int4(-41312, -5312, -22123, 65423)));
        }


        [TestCompiler]
        public static void dot_uint()
        {
            TestUtils.AreEqual(133u, dot(7u, 19u));
            TestUtils.AreEqual(667585376u, dot(61031u, 81312u));
        }

        [TestCompiler]
        public static void dot_uint2()
        {
            TestUtils.AreEqual(322, dot(uint2(7u, 9u), uint2(19u, 21u)));
            TestUtils.AreEqual(734165984u, dot(uint2(61031u, 12534u), uint2(81312u, 5312u)));
        }

        [TestCompiler]
        public static void dot_uint3()
        {
            TestUtils.AreEqual(582u, dot(uint3(7u, 9u, 13u), uint3(19u, 21u, 20u)));
            TestUtils.AreEqual(937940937u, dot(uint3(61031u, 12534u, 9211u), uint3(81312u, 5312u, 22123u)));
        }

        [TestCompiler]
        public static void dot_uint4()
        {
            TestUtils.AreEqual(990u, dot(uint4(7u, 9u, 13u, 17u), uint4(19u, 21u, 20u, 24u)));
            TestUtils.AreEqual(3104881543u, dot(uint4(61031u, 12534u, 9211u, 33122u), uint4(81312u, 5312u, 22123u, 65423u)));
        }


        [TestCompiler]
        public static void dot_float()
        {
            TestUtils.AreEqual(7.32f, dot(1.2f, 6.1f), 1, false);
            TestUtils.AreEqual(-7.32f, dot(1.2f, -6.1f), 1, false);
            TestUtils.AreEqual(7.32e37f, dot(1.2e19f, 6.1e18f), 1, false);
            TestUtils.AreEqual(-7.32e37f, dot(-1.2e19f, 6.1e18f), 1, false);
            TestUtils.AreEqual(float.PositiveInfinity, dot(1.2e19f, 6.1e19f), 0, false);
            TestUtils.AreEqual(float.NegativeInfinity, dot(1.2e19f, -6.1e19f), 0, false);
        }

        [TestCompiler]
        public static void dot_float2()
        {
            TestUtils.AreEqual(57.92f, dot(float2(1.2f, 5.5f), float2(6.1f, 9.2f)), 1, false);
            TestUtils.AreEqual(-57.92f, dot(float2(-1.2f, 5.5f), float2(6.1f, -9.2f)), 1, false);
            TestUtils.AreEqual(5.792e37f, dot(float2(1.2e18f, 5.5e18f), float2(6.1e18f, 9.2e18f)), 1, false);
            TestUtils.AreEqual(-5.792e37f, dot(float2(-1.2e18f, 5.5e18f), float2(6.1e18f, -9.2e18f)), 1, false);
            TestUtils.AreEqual(float.PositiveInfinity, dot(float2(1.2e18f, 5.5e18f), float2(6.1e19f, 9.2e19f)), 1, false);
            TestUtils.AreEqual(float.NegativeInfinity, dot(float2(-1.2e18f, 5.5e18f), float2(6.1e19f, -9.2e19f)), 1, false);
        }

        [TestCompiler]
        public static void dot_float3()
        {
            TestUtils.AreEqual(67.1f, dot(float3(1.2f, 5.5f, 3.4f), float3(6.1f, 9.2f, 2.7f)), 8, false);
            TestUtils.AreEqual(-67.1f, dot(float3(-1.2f, 5.5f, -3.4f), float3(6.1f, -9.2f, 2.7f)), 8, false);
            TestUtils.AreEqual(6.71e37f, dot(float3(1.2e18f, 5.5e18f, 3.4e18f), float3(6.1e18f, 9.2e18f, 2.7e18f)), 1, false);
            TestUtils.AreEqual(-6.71e37f, dot(float3(-1.2e18f, 5.5e18f, 3.4e18f), float3(6.1e18f, -9.2e18f, -2.7e18f)), 1, false);
            TestUtils.AreEqual(float.PositiveInfinity, dot(float3(1.2e18f, 5.5e18f, 3.4e18f), float3(6.1e19f, 9.2e19f, 2.7e19f)), 1, false);
            TestUtils.AreEqual(float.NegativeInfinity, dot(float3(-1.2e18f, 5.5e18f, 3.4e18f), float3(6.1e19f, -9.2e19f, -2.7e19f)), 1, false);
        }

        [TestCompiler]
        public static void dot_float4()
        {
            TestUtils.AreEqual(68.57f, dot(float4(1.2f, 5.5f, 3.4f, 4.9f), float4(6.1f, 9.2f, 2.7f, 0.3f)), 8, false);
            TestUtils.AreEqual(-68.57f, dot(float4(-1.2f, 5.5f, -3.4f, 4.9f), float4(6.1f, -9.2f, 2.7f, -0.3f)), 8, false);
            TestUtils.AreEqual(6.857e37f, dot(float4(1.2e18f, 5.5e18f, 3.4e18f, 4.9e18f), float4(6.1e18f, 9.2e18f, 2.7e18f, 3e17f)), 1, false);
            TestUtils.AreEqual(-6.857e37f, dot(float4(-1.2e18f, 5.5e18f, 3.4e18f, -4.9e18f), float4(6.1e18f, -9.2e18f, -2.7e18f, 3e17f)), 1, false);
            TestUtils.AreEqual(float.PositiveInfinity, dot(float4(1.2e18f, 5.5e18f, 3.4e18f, 4.9e18f), float4(6.1e19f, 9.2e19f, 2.7e19f, 3e18f)), 1, false);
            TestUtils.AreEqual(float.NegativeInfinity, dot(float4(-1.2e18f, 5.5e18f, 3.4e18f, -4.9e18f), float4(6.1e19f, -9.2e19f, -2.7e19f, 3e18f)), 1, false);
        }


        public static void dot_double()
        {
            TestUtils.AreEqual(7.32, dot(1.2, 6.1), 1, false);
            TestUtils.AreEqual(-7.32, dot(1.2, -6.1), 1, false);
            TestUtils.AreEqual(7.32e37, dot(1.2e19, 6.1e18), 1, false);
            TestUtils.AreEqual(-7.32e37, dot(-1.2e19, 6.1e18), 1, false);
            TestUtils.AreEqual(double.PositiveInfinity, dot(1.2e19, 6.1e19), 0, false);
            TestUtils.AreEqual(double.NegativeInfinity, dot(1.2e19, -6.1e19), 0, false);
        }

        [TestCompiler]
        public static void dot_double2()
        {
            TestUtils.AreEqual(57.92, dot(double2(1.2, 5.5), double2(6.1, 9.2)), 1, false);
            TestUtils.AreEqual(-57.92, dot(double2(-1.2, 5.5), double2(6.1, -9.2)), 1, false);
            TestUtils.AreEqual(5.792e307, dot(double2(1.2e153, 5.5e153), double2(6.1e153, 9.2e153)), 1, false);
            TestUtils.AreEqual(-5.792e307, dot(double2(-1.2e153, 5.5e153), double2(6.1e153, -9.2e153)), 1, false);
            TestUtils.AreEqual(double.PositiveInfinity, dot(double2(1.2e153, 5.5e153), double2(6.1e154, 9.2e154)), 1, false);
            TestUtils.AreEqual(double.NegativeInfinity, dot(double2(-1.2e153, 5.5e153), double2(6.1e154, -9.2e154)), 1, false);
        }

        [TestCompiler]
        public static void dot_double3()
        {
            TestUtils.AreEqual(67.1, dot(double3(1.2, 5.5, 3.4), double3(6.1, 9.2, 2.7)), 8, false);
            TestUtils.AreEqual(-67.1, dot(double3(-1.2, 5.5, -3.4), double3(6.1, -9.2, 2.7)), 8, false);
            TestUtils.AreEqual(6.71e307, dot(double3(1.2e153, 5.5e153, 3.4e153), double3(6.1e153, 9.2e153, 2.7e153)), 1, false);
            TestUtils.AreEqual(-6.71e307, dot(double3(-1.2e153, 5.5e153, 3.4e153), double3(6.1e153, -9.2e153, -2.7e153)), 1, false);
            TestUtils.AreEqual(double.PositiveInfinity, dot(double3(1.2e153, 5.5e153, 3.4e153), double3(6.1e154, 9.2e154, 2.7e154)), 1, false);
            TestUtils.AreEqual(double.NegativeInfinity, dot(double3(-1.2e153, 5.5e153, 3.4e153), double3(6.1e154, -9.2e154, -2.7e154)), 1, false);
        }

        [TestCompiler]
        public static void dot_double4()
        {
            TestUtils.AreEqual(68.57, dot(double4(1.2, 5.5, 3.4, 4.9), double4(6.1, 9.2, 2.7, 0.3)), 8, false);
            TestUtils.AreEqual(-68.57, dot(double4(-1.2, 5.5, -3.4, 4.9), double4(6.1, -9.2, 2.7, -0.3)), 8, false);
            TestUtils.AreEqual(6.857e307, dot(double4(1.2e153, 5.5e153, 3.4e153, 4.9e153), double4(6.1e153, 9.2e153, 2.7e153, 3e152)), 1, false);
            TestUtils.AreEqual(-6.857e307, dot(double4(-1.2e153, 5.5e153, 3.4e153, -4.9e153), double4(6.1e153, -9.2e153, -2.7e153, 3e152)), 1, false);
            TestUtils.AreEqual(double.PositiveInfinity, dot(double4(1.2e153, 5.5e153, 3.4e153, 4.9e153), double4(6.1e154, 9.2e154, 2.7e154, 3e153)), 1, false);
            TestUtils.AreEqual(double.NegativeInfinity, dot(double4(-1.2e153, 5.5e153, 3.4e153, -4.9e153), double4(6.1e154, -9.2e154, -2.7e154, 3e153)), 1, false);
        }

        [TestCompiler]
        public static void cmin_int2()
        {
            TestUtils.AreEqual(100, cmin(int2(100, 200)));
            TestUtils.AreEqual(-200, cmin(int2(100, -200)));
            TestUtils.AreEqual(0, cmin(int2(int.MaxValue, 0)));
            TestUtils.AreEqual(int.MinValue, cmin(int2(int.MinValue, 0)));
            TestUtils.AreEqual(int.MinValue, cmin(int2(int.MaxValue, int.MinValue)));
        }


        [TestCompiler]
        public static void cmin_int3()
        {
            TestUtils.AreEqual(100, cmin(int3(100, 200, 300)));
            TestUtils.AreEqual(-200, cmin(int3(100, -200, 300)));
            TestUtils.AreEqual(0, cmin(int3(int.MaxValue, 0, 7)));
            TestUtils.AreEqual(int.MinValue, cmin(int3(int.MinValue, 0, 7)));
            TestUtils.AreEqual(int.MinValue, cmin(int3(int.MaxValue, int.MinValue, 0)));
        }


        [TestCompiler]
        public static void cmin_int4()
        {
            TestUtils.AreEqual(100, cmin(int4(100, 200, 300, 400)));
            TestUtils.AreEqual(-400, cmin(int4(100, -200, 300, -400)));
            TestUtils.AreEqual(0, cmin(int4(int.MaxValue, 0, 7, 19)));
            TestUtils.AreEqual(int.MinValue, cmin(int4(int.MinValue, 0, 7, 19)));
            TestUtils.AreEqual(int.MinValue, cmin(int4(int.MaxValue, int.MinValue, 0, 19)));
        }

        [TestCompiler]
        public static void cmin_uint2()
        {
            TestUtils.AreEqual(100u, cmin(uint2(100u, 200u)));
            TestUtils.AreEqual(100u, cmin(uint2(100u, uint.MaxValue)));
            TestUtils.AreEqual(uint.MinValue, cmin(uint2(uint.MinValue, uint.MaxValue)));
        }


        [TestCompiler]
        public static void cmin_uint3()
        {
            TestUtils.AreEqual(100u, cmin(uint3(100u, 200u, 300u)));
            TestUtils.AreEqual(100u, cmin(uint3(uint.MaxValue, 100u, 300u)));
            TestUtils.AreEqual(uint.MinValue, cmin(uint3(7u, uint.MinValue, uint.MaxValue)));
        }


        [TestCompiler]
        public static void cmin_uint4()
        {
            TestUtils.AreEqual(100u, cmin(uint4(100u, 200u, 300u, 400u)));
            TestUtils.AreEqual(100u, cmin(uint4(300u, 100u, uint.MaxValue, 200u)));
            TestUtils.AreEqual(uint.MinValue, cmin(uint4(19u, uint.MinValue, uint.MaxValue, 7u)));
        }

        [TestCompiler]
        public static void cmin_float2()
        {
            TestUtils.AreEqual(-0.5f, cmin(float2(5.2f, -0.5f)));
            TestUtils.AreEqual(float.NegativeInfinity, cmin(float2(float.NegativeInfinity, float.PositiveInfinity)));
            TestUtils.AreEqual(float.NegativeInfinity, cmin(float2(float.NegativeInfinity, float.NaN)));
            TestUtils.AreEqual(float.NegativeInfinity, cmin(float2(float.NegativeInfinity, 100.0f)));
            TestUtils.AreEqual(100.0f, cmin(float2(float.PositiveInfinity, 100.0f)));

            TestUtils.AreEqual(1.0f, cmin(float2(1.0f, float.NaN)));
            TestUtils.AreEqual(1.0f, cmin(float2(float.NaN, 1.0f)));

            TestUtils.AreEqual(float.PositiveInfinity, cmin(float2(float.PositiveInfinity, float.NaN)));
            TestUtils.AreEqual(float.PositiveInfinity, cmin(float2(float.NaN, float.PositiveInfinity)));

            TestUtils.AreEqual(float.NegativeInfinity, cmin(float2(float.NegativeInfinity, float.NaN)));
            TestUtils.AreEqual(float.NegativeInfinity, cmin(float2(float.NaN, float.NegativeInfinity)));
        }

        [TestCompiler]
        public static void cmin_float3()
        {
            TestUtils.AreEqual(-1.2f, cmin(float3(5.2f, -0.5f, -1.2f)));
            TestUtils.AreEqual(float.NegativeInfinity, cmin(float3(float.NegativeInfinity, float.PositiveInfinity, 100.0f)));
            TestUtils.AreEqual(float.NegativeInfinity, cmin(float3(float.NegativeInfinity, float.NaN, 100.0f)));

            TestUtils.AreEqual(1.0f, cmin(float3(1.0f, float.NaN, float.NaN)));
            TestUtils.AreEqual(1.0f, cmin(float3(float.NaN, 1.0f, float.NaN)));
            TestUtils.AreEqual(1.0f, cmin(float3(float.NaN, float.NaN, 1.0f)));

            TestUtils.AreEqual(float.PositiveInfinity, cmin(float3(float.PositiveInfinity, float.NaN, float.NaN)));
            TestUtils.AreEqual(float.PositiveInfinity, cmin(float3(float.NaN, float.PositiveInfinity, float.NaN)));
            TestUtils.AreEqual(float.PositiveInfinity, cmin(float3(float.NaN, float.NaN, float.PositiveInfinity)));

            TestUtils.AreEqual(float.NegativeInfinity, cmin(float3(float.NegativeInfinity, float.NaN, float.NaN)));
            TestUtils.AreEqual(float.NegativeInfinity, cmin(float3(float.NaN, float.NegativeInfinity, float.NaN)));
            TestUtils.AreEqual(float.NegativeInfinity, cmin(float3(float.NaN, float.NaN, float.NegativeInfinity)));
        }

        [TestCompiler]
        public static void cmin_float4()
        {
            TestUtils.AreEqual(-1.2f, cmin(float4(5.2f, -0.5f, -1.2f, 2.3f)));
            TestUtils.AreEqual(float.NegativeInfinity, cmin(float4(float.NegativeInfinity, float.PositiveInfinity, 100.0f, float.NaN)));

            TestUtils.AreEqual(1.0f, cmin(float4(1.0f, float.NaN, float.NaN, float.NaN)));
            TestUtils.AreEqual(1.0f, cmin(float4(float.NaN, 1.0f, float.NaN, float.NaN)));
            TestUtils.AreEqual(1.0f, cmin(float4(float.NaN, float.NaN, 1.0f, float.NaN)));
            TestUtils.AreEqual(1.0f, cmin(float4(float.NaN, float.NaN, float.NaN, 1.0f)));

            TestUtils.AreEqual(float.PositiveInfinity, cmin(float4(float.PositiveInfinity, float.NaN, float.NaN, float.NaN)));
            TestUtils.AreEqual(float.PositiveInfinity, cmin(float4(float.NaN, float.PositiveInfinity, float.NaN, float.NaN)));
            TestUtils.AreEqual(float.PositiveInfinity, cmin(float4(float.NaN, float.NaN, float.PositiveInfinity, float.NaN)));
            TestUtils.AreEqual(float.PositiveInfinity, cmin(float4(float.NaN, float.NaN, float.NaN, float.PositiveInfinity)));

            TestUtils.AreEqual(float.NegativeInfinity, cmin(float4(float.NegativeInfinity, float.NaN, float.NaN, float.NaN)));
            TestUtils.AreEqual(float.NegativeInfinity, cmin(float4(float.NaN, float.NegativeInfinity, float.NaN, float.NaN)));
            TestUtils.AreEqual(float.NegativeInfinity, cmin(float4(float.NaN, float.NaN, float.NegativeInfinity, float.NaN)));
            TestUtils.AreEqual(float.NegativeInfinity, cmin(float4(float.NaN, float.NaN, float.NaN, float.NegativeInfinity)));
        }


        [TestCompiler]
        public static void cmin_double2()
        {
            TestUtils.AreEqual(-0.5, cmin(double2(5.2, -0.5)));
            TestUtils.AreEqual(-0.5e100, cmin(double2(5.2e100, -0.5e100)));
            TestUtils.AreEqual(double.NegativeInfinity, cmin(double2(double.NegativeInfinity, double.PositiveInfinity)));
            TestUtils.AreEqual(double.NegativeInfinity, cmin(double2(double.NegativeInfinity, double.NaN)));
            TestUtils.AreEqual(double.NegativeInfinity, cmin(double2(double.NegativeInfinity, 100.0)));
            TestUtils.AreEqual(100.0, cmin(double2(double.PositiveInfinity, 100.0)));

            TestUtils.AreEqual(1.0, cmin(double2(1.0, double.NaN)));
            TestUtils.AreEqual(1.0, cmin(double2(double.NaN, 1.0)));

            TestUtils.AreEqual(double.PositiveInfinity, cmin(double2(double.PositiveInfinity, double.NaN)));
            TestUtils.AreEqual(double.PositiveInfinity, cmin(double2(double.NaN, double.PositiveInfinity)));

            TestUtils.AreEqual(double.NegativeInfinity, cmin(double2(double.NegativeInfinity, double.NaN)));
            TestUtils.AreEqual(double.NegativeInfinity, cmin(double2(double.NaN, double.NegativeInfinity)));
        }

        [TestCompiler]
        public static void cmin_double3()
        {
            TestUtils.AreEqual(-1.2, cmin(double3(5.2, -0.5, -1.2)));
            TestUtils.AreEqual(-1.2e100, cmin(double3(5.2e100, -0.5e100, -1.2e100)));
            TestUtils.AreEqual(double.NegativeInfinity, cmin(double3(double.NegativeInfinity, double.PositiveInfinity, 100.0)));
            TestUtils.AreEqual(double.NegativeInfinity, cmin(double3(double.NegativeInfinity, double.NaN, 100.0)));

            TestUtils.AreEqual(1.0, cmin(double3(1.0, double.NaN, double.NaN)));
            TestUtils.AreEqual(1.0, cmin(double3(double.NaN, 1.0, double.NaN)));
            TestUtils.AreEqual(1.0, cmin(double3(double.NaN, double.NaN, 1.0)));

            TestUtils.AreEqual(double.PositiveInfinity, cmin(double3(double.PositiveInfinity, double.NaN, double.NaN)));
            TestUtils.AreEqual(double.PositiveInfinity, cmin(double3(double.NaN, double.PositiveInfinity, double.NaN)));
            TestUtils.AreEqual(double.PositiveInfinity, cmin(double3(double.NaN, double.NaN, double.PositiveInfinity)));

            TestUtils.AreEqual(double.NegativeInfinity, cmin(double3(double.NegativeInfinity, double.NaN, double.NaN)));
            TestUtils.AreEqual(double.NegativeInfinity, cmin(double3(double.NaN, double.NegativeInfinity, double.NaN)));
            TestUtils.AreEqual(double.NegativeInfinity, cmin(double3(double.NaN, double.NaN, double.NegativeInfinity)));
        }

        [TestCompiler]
        public static void cmin_double4()
        {
            TestUtils.AreEqual(-1.2, cmin(double4(5.2, -0.5, -1.2, 2.3)));
            TestUtils.AreEqual(-1.2e100, cmin(double4(5.2e100, -0.5e100, -1.2e100, 2.3e100)));

            TestUtils.AreEqual(double.NegativeInfinity, cmin(double4(double.NegativeInfinity, double.PositiveInfinity, 100.0, double.NaN)));

            TestUtils.AreEqual(1.0, cmin(double4(1.0, double.NaN, double.NaN, double.NaN)));
            TestUtils.AreEqual(1.0f, cmin(double4(double.NaN, 1.0, double.NaN, double.NaN)));
            TestUtils.AreEqual(1.0f, cmin(double4(double.NaN, double.NaN, 1.0, double.NaN)));
            TestUtils.AreEqual(1.0f, cmin(double4(double.NaN, double.NaN, double.NaN, 1.0)));

            TestUtils.AreEqual(double.PositiveInfinity, cmin(double4(double.PositiveInfinity, double.NaN, double.NaN, double.NaN)));
            TestUtils.AreEqual(double.PositiveInfinity, cmin(double4(double.NaN, double.PositiveInfinity, double.NaN, double.NaN)));
            TestUtils.AreEqual(double.PositiveInfinity, cmin(double4(double.NaN, double.NaN, double.PositiveInfinity, double.NaN)));
            TestUtils.AreEqual(double.PositiveInfinity, cmin(double4(double.NaN, double.NaN, double.NaN, double.PositiveInfinity)));

            TestUtils.AreEqual(double.NegativeInfinity, cmin(double4(double.NegativeInfinity, double.NaN, double.NaN, double.NaN)));
            TestUtils.AreEqual(double.NegativeInfinity, cmin(double4(double.NaN, double.NegativeInfinity, double.NaN, double.NaN)));
            TestUtils.AreEqual(double.NegativeInfinity, cmin(double4(double.NaN, double.NaN, double.NegativeInfinity, double.NaN)));
            TestUtils.AreEqual(double.NegativeInfinity, cmin(double4(double.NaN, double.NaN, double.NaN, double.NegativeInfinity)));
        }


        [TestCompiler]
        public static void cmax_int2()
        {
            TestUtils.AreEqual(200, cmax(int2(100, 200)));
            TestUtils.AreEqual(100, cmax(int2(100, -200)));
            TestUtils.AreEqual(int.MaxValue, cmax(int2(int.MaxValue, 0)));
            TestUtils.AreEqual(0, cmax(int2(int.MinValue, 0)));
            TestUtils.AreEqual(int.MaxValue, cmax(int2(int.MaxValue, int.MinValue)));
        }


        [TestCompiler]
        public static void cmax_int3()
        {
            TestUtils.AreEqual(300, cmax(int3(100, 200, 300)));
            TestUtils.AreEqual(300, cmax(int3(100, -200, 300)));
            TestUtils.AreEqual(int.MaxValue, cmax(int3(int.MaxValue, 0, 7)));
            TestUtils.AreEqual(7, cmax(int3(int.MinValue, 0, 7)));
            TestUtils.AreEqual(int.MaxValue, cmax(int3(int.MaxValue, int.MinValue, 0)));
        }


        [TestCompiler]
        public static void cmax_int4()
        {
            TestUtils.AreEqual(400, cmax(int4(100, 200, 300, 400)));
            TestUtils.AreEqual(300, cmax(int4(100, -200, 300, -400)));
            TestUtils.AreEqual(int.MaxValue, cmax(int4(int.MaxValue, 0, 7, 19)));
            TestUtils.AreEqual(19, cmax(int4(int.MinValue, 0, 7, 19)));
            TestUtils.AreEqual(int.MaxValue, cmax(int4(int.MaxValue, int.MinValue, 0, 19)));
        }

        [TestCompiler]
        public static void cmax_uint2()
        {
            TestUtils.AreEqual(200u, cmax(uint2(100u, 200u)));
            TestUtils.AreEqual(uint.MaxValue, cmax(uint2(100u, uint.MaxValue)));
            TestUtils.AreEqual(uint.MaxValue, cmax(uint2(uint.MinValue, uint.MaxValue)));
        }


        [TestCompiler]
        public static void cmax_uint3()
        {
            TestUtils.AreEqual(300u, cmax(uint3(100u, 200u, 300u)));
            TestUtils.AreEqual(uint.MaxValue, cmax(uint3(uint.MaxValue, 100u, 300u)));
            TestUtils.AreEqual(uint.MaxValue, cmax(uint3(7u, uint.MinValue, uint.MaxValue)));
        }


        [TestCompiler]
        public static void cmax_uint4()
        {
            TestUtils.AreEqual(400u, cmax(uint4(100u, 200u, 300u, 400u)));
            TestUtils.AreEqual(uint.MaxValue, cmax(uint4(300u, 100u, uint.MaxValue, 200u)));
            TestUtils.AreEqual(uint.MaxValue, cmax(uint4(19u, uint.MinValue, uint.MaxValue, 7u)));
        }

        [TestCompiler]
        public static void cmax_float2()
        {
            TestUtils.AreEqual(5.2f, cmax(float2(5.2f, -0.5f)));
            TestUtils.AreEqual(float.PositiveInfinity, cmax(float2(float.NegativeInfinity, float.PositiveInfinity)));
            TestUtils.AreEqual(float.NegativeInfinity, cmax(float2(float.NegativeInfinity, float.NaN)));
            TestUtils.AreEqual(100.0f, cmax(float2(float.NegativeInfinity, 100.0f)));
            TestUtils.AreEqual(float.PositiveInfinity, cmax(float2(float.PositiveInfinity, 100.0f)));

            TestUtils.AreEqual(1.0f, cmax(float2(1.0f, float.NaN)));
            TestUtils.AreEqual(1.0f, cmax(float2(float.NaN, 1.0f)));

            TestUtils.AreEqual(float.PositiveInfinity, cmax(float2(float.PositiveInfinity, float.NaN)));
            TestUtils.AreEqual(float.PositiveInfinity, cmax(float2(float.NaN, float.PositiveInfinity)));

            TestUtils.AreEqual(float.NegativeInfinity, cmax(float2(float.NegativeInfinity, float.NaN)));
            TestUtils.AreEqual(float.NegativeInfinity, cmax(float2(float.NaN, float.NegativeInfinity)));
        }

        [TestCompiler]
        public static void cmax_float3()
        {
            TestUtils.AreEqual(5.2f, cmax(float3(5.2f, -0.5f, -1.2f)));
            TestUtils.AreEqual(float.PositiveInfinity, cmax(float3(float.NegativeInfinity, float.PositiveInfinity, 100.0f)));
            TestUtils.AreEqual(100.0f, cmax(float3(float.NegativeInfinity, float.NaN, 100.0f)));

            TestUtils.AreEqual(1.0f, cmax(float3(1.0f, float.NaN, float.NaN)));
            TestUtils.AreEqual(1.0f, cmax(float3(float.NaN, 1.0f, float.NaN)));
            TestUtils.AreEqual(1.0f, cmax(float3(float.NaN, float.NaN, 1.0f)));

            TestUtils.AreEqual(float.PositiveInfinity, cmax(float3(float.PositiveInfinity, float.NaN, float.NaN)));
            TestUtils.AreEqual(float.PositiveInfinity, cmax(float3(float.NaN, float.PositiveInfinity, float.NaN)));
            TestUtils.AreEqual(float.PositiveInfinity, cmax(float3(float.NaN, float.NaN, float.PositiveInfinity)));

            TestUtils.AreEqual(float.NegativeInfinity, cmax(float3(float.NegativeInfinity, float.NaN, float.NaN)));
            TestUtils.AreEqual(float.NegativeInfinity, cmax(float3(float.NaN, float.NegativeInfinity, float.NaN)));
            TestUtils.AreEqual(float.NegativeInfinity, cmax(float3(float.NaN, float.NaN, float.NegativeInfinity)));
        }

        [TestCompiler]
        public static void cmax_float4()
        {
            TestUtils.AreEqual(5.2f, cmax(float4(5.2f, -0.5f, -1.2f, 2.3f)));
            TestUtils.AreEqual(float.PositiveInfinity, cmax(float4(float.NegativeInfinity, float.PositiveInfinity, 100.0f, float.NaN)));

            TestUtils.AreEqual(1.0f, cmax(float4(1.0f, float.NaN, float.NaN, float.NaN)));
            TestUtils.AreEqual(1.0f, cmax(float4(float.NaN, 1.0f, float.NaN, float.NaN)));
            TestUtils.AreEqual(1.0f, cmax(float4(float.NaN, float.NaN, 1.0f, float.NaN)));
            TestUtils.AreEqual(1.0f, cmax(float4(float.NaN, float.NaN, float.NaN, 1.0f)));

            TestUtils.AreEqual(float.PositiveInfinity, cmax(float4(float.PositiveInfinity, float.NaN, float.NaN, float.NaN)));
            TestUtils.AreEqual(float.PositiveInfinity, cmax(float4(float.NaN, float.PositiveInfinity, float.NaN, float.NaN)));
            TestUtils.AreEqual(float.PositiveInfinity, cmax(float4(float.NaN, float.NaN, float.PositiveInfinity, float.NaN)));
            TestUtils.AreEqual(float.PositiveInfinity, cmax(float4(float.NaN, float.NaN, float.NaN, float.PositiveInfinity)));

            TestUtils.AreEqual(float.NegativeInfinity, cmax(float4(float.NegativeInfinity, float.NaN, float.NaN, float.NaN)));
            TestUtils.AreEqual(float.NegativeInfinity, cmax(float4(float.NaN, float.NegativeInfinity, float.NaN, float.NaN)));
            TestUtils.AreEqual(float.NegativeInfinity, cmax(float4(float.NaN, float.NaN, float.NegativeInfinity, float.NaN)));
            TestUtils.AreEqual(float.NegativeInfinity, cmax(float4(float.NaN, float.NaN, float.NaN, float.NegativeInfinity)));
        }


        [TestCompiler]
        public static void cmax_double2()
        {
            TestUtils.AreEqual(5.2, cmax(double2(5.2, -0.5)));
            TestUtils.AreEqual(5.2e100, cmax(double2(5.2e100, -0.5e100)));
            TestUtils.AreEqual(double.PositiveInfinity, cmax(double2(double.NegativeInfinity, double.PositiveInfinity)));
            TestUtils.AreEqual(double.NegativeInfinity, cmax(double2(double.NegativeInfinity, double.NaN)));
            TestUtils.AreEqual(100.0, cmax(double2(double.NegativeInfinity, 100.0)));
            TestUtils.AreEqual(double.PositiveInfinity, cmax(double2(double.PositiveInfinity, 100.0)));

            TestUtils.AreEqual(1.0, cmax(double2(1.0, double.NaN)));
            TestUtils.AreEqual(1.0, cmax(double2(double.NaN, 1.0)));

            TestUtils.AreEqual(double.PositiveInfinity, cmax(double2(double.PositiveInfinity, double.NaN)));
            TestUtils.AreEqual(double.PositiveInfinity, cmax(double2(double.NaN, double.PositiveInfinity)));

            TestUtils.AreEqual(double.NegativeInfinity, cmax(double2(double.NegativeInfinity, double.NaN)));
            TestUtils.AreEqual(double.NegativeInfinity, cmax(double2(double.NaN, double.NegativeInfinity)));
        }

        [TestCompiler]
        public static void cmax_double3()
        {
            TestUtils.AreEqual(5.2, cmax(double3(5.2, -0.5, -1.2)));
            TestUtils.AreEqual(5.2e100, cmax(double3(5.2e100, -0.5e100, -1.2e100)));
            TestUtils.AreEqual(double.PositiveInfinity, cmax(double3(double.NegativeInfinity, double.PositiveInfinity, 100.0)));
            TestUtils.AreEqual(100.0, cmax(double3(double.NegativeInfinity, double.NaN, 100.0)));

            TestUtils.AreEqual(1.0, cmax(double3(1.0, double.NaN, double.NaN)));
            TestUtils.AreEqual(1.0, cmax(double3(double.NaN, 1.0, double.NaN)));
            TestUtils.AreEqual(1.0, cmax(double3(double.NaN, double.NaN, 1.0)));

            TestUtils.AreEqual(double.PositiveInfinity, cmax(double3(double.PositiveInfinity, double.NaN, double.NaN)));
            TestUtils.AreEqual(double.PositiveInfinity, cmax(double3(double.NaN, double.PositiveInfinity, double.NaN)));
            TestUtils.AreEqual(double.PositiveInfinity, cmax(double3(double.NaN, double.NaN, double.PositiveInfinity)));

            TestUtils.AreEqual(double.NegativeInfinity, cmax(double3(double.NegativeInfinity, double.NaN, double.NaN)));
            TestUtils.AreEqual(double.NegativeInfinity, cmax(double3(double.NaN, double.NegativeInfinity, double.NaN)));
            TestUtils.AreEqual(double.NegativeInfinity, cmax(double3(double.NaN, double.NaN, double.NegativeInfinity)));
        }

        [TestCompiler]
        public static void cmax_double4()
        {
            TestUtils.AreEqual(5.2, cmax(double4(5.2, -0.5, -1.2, 2.3)));
            TestUtils.AreEqual(5.2e100, cmax(double4(5.2e100, -0.5e100, -1.2e100, 2.3e100)));

            TestUtils.AreEqual(double.PositiveInfinity, cmax(double4(double.NegativeInfinity, double.PositiveInfinity, 100.0, double.NaN)));

            TestUtils.AreEqual(1.0, cmax(double4(1.0, double.NaN, double.NaN, double.NaN)));
            TestUtils.AreEqual(1.0f, cmax(double4(double.NaN, 1.0, double.NaN, double.NaN)));
            TestUtils.AreEqual(1.0f, cmax(double4(double.NaN, double.NaN, 1.0, double.NaN)));
            TestUtils.AreEqual(1.0f, cmax(double4(double.NaN, double.NaN, double.NaN, 1.0)));

            TestUtils.AreEqual(double.PositiveInfinity, cmax(double4(double.PositiveInfinity, double.NaN, double.NaN, double.NaN)));
            TestUtils.AreEqual(double.PositiveInfinity, cmax(double4(double.NaN, double.PositiveInfinity, double.NaN, double.NaN)));
            TestUtils.AreEqual(double.PositiveInfinity, cmax(double4(double.NaN, double.NaN, double.PositiveInfinity, double.NaN)));
            TestUtils.AreEqual(double.PositiveInfinity, cmax(double4(double.NaN, double.NaN, double.NaN, double.PositiveInfinity)));

            TestUtils.AreEqual(double.NegativeInfinity, cmax(double4(double.NegativeInfinity, double.NaN, double.NaN, double.NaN)));
            TestUtils.AreEqual(double.NegativeInfinity, cmax(double4(double.NaN, double.NegativeInfinity, double.NaN, double.NaN)));
            TestUtils.AreEqual(double.NegativeInfinity, cmax(double4(double.NaN, double.NaN, double.NegativeInfinity, double.NaN)));
            TestUtils.AreEqual(double.NegativeInfinity, cmax(double4(double.NaN, double.NaN, double.NaN, double.NegativeInfinity)));
        }

        [TestCompiler]
        public static void csum_int2()
        {
            TestUtils.AreEqual(300, csum(int2(100, 200)));
            TestUtils.AreEqual(-100, csum(int2(100, -200)));
            TestUtils.AreEqual(int.MinValue + 6, csum(int2(int.MaxValue, 7)));
            TestUtils.AreEqual(int.MaxValue - 6, csum(int2(int.MinValue, -7)));
            TestUtils.AreEqual(-1, csum(int2(int.MaxValue, int.MinValue)));
        }


        [TestCompiler]
        public static void csum_int3()
        {
            TestUtils.AreEqual(600, csum(int3(100, 200, 300)));
            TestUtils.AreEqual(-400, csum(int3(100, -200, -300)));
            TestUtils.AreEqual(int.MinValue + 17, csum(int3(int.MaxValue, 7, 11)));
            TestUtils.AreEqual(int.MaxValue - 17, csum(int3(int.MinValue, -7, -11)));
            TestUtils.AreEqual(-1, csum(int3(int.MaxValue, int.MinValue, 0)));
        }


        [TestCompiler]
        public static void csum_int4()
        {
            TestUtils.AreEqual(1000, csum(int4(100, 200, 300, 400)));
            TestUtils.AreEqual(-200, csum(int4(100, -200, 300, -400)));
            TestUtils.AreEqual(int.MinValue + 36, csum(int4(int.MaxValue, 7, 11, 19)));
            TestUtils.AreEqual(int.MaxValue - 36, csum(int4(int.MinValue, -7, -11, -19)));
            TestUtils.AreEqual(-1, csum(int4(int.MaxValue, int.MinValue, 0, 0)));
        }

        [TestCompiler]
        public static void csum_uint2()
        {
            TestUtils.AreEqual(300u, csum(uint2(100u, 200u)));
            TestUtils.AreEqual(6u, csum(uint2(uint.MaxValue, 7u)));
        }


        [TestCompiler]
        public static void csum_uint3()
        {
            TestUtils.AreEqual(600u, csum(uint3(100u, 200u, 300u)));
            TestUtils.AreEqual(25u, csum(uint3(uint.MaxValue, 7u, 19u)));
        }


        [TestCompiler]
        public static void csum_uint4()
        {
            TestUtils.AreEqual(1000u, csum(uint4(100u, 200u, 300u, 400u)));
            TestUtils.AreEqual(36u, csum(uint4(uint.MaxValue, 7u, 11u, 19u)));
        }

        [TestCompiler]
        public static void csum_float2()
        {
            TestUtils.AreEqual(0.7f, csum(float2(2.2f, -1.5f)), 4, false);
            TestUtils.AreEqual(-7e37f, csum(float2(-2.2e38f, 1.5e38f)), 4, false);
            TestUtils.AreEqual(float.NegativeInfinity, csum(float2(-2.2e38f, -1.5e38f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, csum(float2( 2.2e38f, 1.5e38f)), 0, false);

            TestUtils.AreEqual(float.NaN, csum(float2(float.NegativeInfinity, float.PositiveInfinity)), 0, false);
            TestUtils.AreEqual(float.NaN, csum(float2(float.NegativeInfinity, float.NaN)), 0, false);
            TestUtils.AreEqual(float.NegativeInfinity, csum(float2(float.NegativeInfinity, 100.0f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, csum(float2(float.PositiveInfinity, 100.0f)), 0, false);

            TestUtils.AreEqual(float.NaN, csum(float2(1.0f, float.NaN)), 0, false);
            TestUtils.AreEqual(float.NaN, csum(float2(float.NaN, 1.0f)), 0, false);

            TestUtils.AreEqual(float.PositiveInfinity, csum(float2(float.PositiveInfinity,  1.0f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, csum(float2(-1.0f, float.PositiveInfinity)), 0, false);

            TestUtils.AreEqual(float.NegativeInfinity, csum(float2(float.NegativeInfinity, 1.0f)), 0, false);
            TestUtils.AreEqual(float.NegativeInfinity, csum(float2(-1.0f, float.NegativeInfinity)), 0, false);
        }

        [TestCompiler]
        public static void csum_float3()
        {
            TestUtils.AreEqual(1.9f, csum(float3(2.2f, -1.5f, 1.2f)), 4, false);
            TestUtils.AreEqual(1.9e38f, csum(float3(2.2e38f, -1.5e38f, 1.2e38f)), 4, false);
            TestUtils.AreEqual(float.NegativeInfinity, csum(float3(-2.2e38f, -1.5e38f, -1.2e38f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, csum(float3(2.2e38f, 1.5e38f, 1.2e38f)), 0, false);

            TestUtils.AreEqual(float.NaN, csum(float3(float.NegativeInfinity, float.PositiveInfinity, 100.0f)), 0, false);
            TestUtils.AreEqual(float.NaN, csum(float3(float.NegativeInfinity, float.NaN, 100.0f)), 0, false);

            TestUtils.AreEqual(float.NaN, csum(float3(float.NaN, 1.0f, 1.0f)), 0, false);
            TestUtils.AreEqual(float.NaN, csum(float3(1.0f, float.NaN, 1.0f)), 0, false);
            TestUtils.AreEqual(float.NaN, csum(float3(1.0f, 1.0f, float.NaN)), 0, false);

            TestUtils.AreEqual(float.PositiveInfinity, csum(float3(float.PositiveInfinity, 1.0f, -2.0f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, csum(float3(1.0f, float.PositiveInfinity, -2.0f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, csum(float3(-2.0f, 1.0f, float.PositiveInfinity)), 0, false);

            TestUtils.AreEqual(float.NegativeInfinity, csum(float3(float.NegativeInfinity, 1.0f, -2.0f)), 0, false);
            TestUtils.AreEqual(float.NegativeInfinity, csum(float3(-1.0f, float.NegativeInfinity, 2.0f)), 0, false);
            TestUtils.AreEqual(float.NegativeInfinity, csum(float3(-2.0f, 1.0f, float.NegativeInfinity)), 0, false);
        }

        [TestCompiler]
        public static void csum_float4()
        {
            TestUtils.AreEqual(1.2f, csum(float4(2.2f, -1.5f, 1.2f, -0.7f)), 4, false);
            TestUtils.AreEqual(1.2e38f, csum(float4(2.2e38f, -1.5e38f, 1.2e38f, -0.7e38f)), 4, false);
            TestUtils.AreEqual(float.NegativeInfinity, csum(float4(-2.2e38f, -1.5e38f, -1.2e38f, -0.7e38f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, csum(float4(2.2e38f, 1.5e38f, 1.2e38f, 0.7e38f)), 0, false);

            TestUtils.AreEqual(float.NaN, csum(float4(float.NegativeInfinity, float.PositiveInfinity, 100.0f, 200.0f)), 0, false);

            TestUtils.AreEqual(float.NaN, csum(float4(float.NaN, 1.0f, 1.0f, 1.0f)), 0, false);
            TestUtils.AreEqual(float.NaN, csum(float4(1.0f, float.NaN, 1.0f, 1.0f)), 0, false);
            TestUtils.AreEqual(float.NaN, csum(float4(1.0f, 1.0f, float.NaN, 1.0f)), 0, false);
            TestUtils.AreEqual(float.NaN, csum(float4(1.0f, 1.0f, 1.0f, float.NaN)), 0, false);

            TestUtils.AreEqual(float.PositiveInfinity, csum(float4(float.PositiveInfinity, 1.0f, -2.0f, 3.0f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, csum(float4(1.0f, float.PositiveInfinity, -2.0f, 3.0f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, csum(float4(1.0f, -2.0f, float.PositiveInfinity, 3.0f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, csum(float4(1.0f, -2.0f, 3.0f, float.PositiveInfinity)), 0, false);

            TestUtils.AreEqual(float.NegativeInfinity, csum(float4(float.NegativeInfinity, 1.0f, -2.0f, 3.0f)), 0, false);
            TestUtils.AreEqual(float.NegativeInfinity, csum(float4(1.0f, float.NegativeInfinity, -2.0f, 3.0f)), 0, false);
            TestUtils.AreEqual(float.NegativeInfinity, csum(float4(1.0f, -2.0f, float.NegativeInfinity, 3.0f)), 0, false);
            TestUtils.AreEqual(float.NegativeInfinity, csum(float4(1.0f, -2.0f, -3.0f, float.NegativeInfinity)), 0, false);
        }


        [TestCompiler]
        public static void csum_double2()
        {
            TestUtils.AreEqual(0.7, csum(double2(2.2, -1.5)), 4, false);
            TestUtils.AreEqual(-7e306, csum(double2(-2.2e307, 1.5e307)), 4, false);
            TestUtils.AreEqual(double.NegativeInfinity, csum(double2(-1.2e308, -0.7e308)));
            TestUtils.AreEqual(double.PositiveInfinity, csum(double2(1.2e308, 0.7e308)));

            TestUtils.AreEqual(double.NaN, csum(double2(double.NegativeInfinity, double.PositiveInfinity)));
            TestUtils.AreEqual(double.NaN, csum(double2(double.NegativeInfinity, double.NaN)));
            TestUtils.AreEqual(double.NegativeInfinity, csum(double2(double.NegativeInfinity, 100.0)));
            TestUtils.AreEqual(double.PositiveInfinity, csum(double2(double.PositiveInfinity, 100.0)));

            TestUtils.AreEqual(double.NaN, csum(double2(1.0, double.NaN)));
            TestUtils.AreEqual(double.NaN, csum(double2(double.NaN, 1.0)));

            TestUtils.AreEqual(double.PositiveInfinity, csum(double2(double.PositiveInfinity, 1.0)));
            TestUtils.AreEqual(double.PositiveInfinity, csum(double2(-1.0, double.PositiveInfinity)));

            TestUtils.AreEqual(double.NegativeInfinity, csum(double2(double.NegativeInfinity, 1.0)));
            TestUtils.AreEqual(double.NegativeInfinity, csum(double2(-1.0, double.NegativeInfinity)));
        }

        [TestCompiler]
        public static void csum_double3()
        {
            TestUtils.AreEqual(1.9, csum(double3(2.2, -1.5, 1.2)), 4, false);
            TestUtils.AreEqual(1.9e307, csum(double3(2.2e307, -1.5e307, 1.2e307)), 4, false);
            TestUtils.AreEqual(double.NegativeInfinity, csum(double3(-1.2e308, -0.7e308, -1.4e308)));
            TestUtils.AreEqual(double.PositiveInfinity, csum(double3(1.2e308, 0.7e308, 1.4e308)));

            TestUtils.AreEqual(double.NaN, csum(double3(double.NegativeInfinity, double.PositiveInfinity, 100.0)));
            TestUtils.AreEqual(double.NaN, csum(double3(double.NegativeInfinity, double.NaN, 100.0)));

            TestUtils.AreEqual(double.NaN, csum(double3(double.NaN, 1.0, 1.0)));
            TestUtils.AreEqual(double.NaN, csum(double3(1.0, double.NaN, 1.0)));
            TestUtils.AreEqual(double.NaN, csum(double3(1.0, 1.0, double.NaN)));

            TestUtils.AreEqual(double.PositiveInfinity, csum(double3(double.PositiveInfinity, 1.0, -2.0)));
            TestUtils.AreEqual(double.PositiveInfinity, csum(double3(1.0, double.PositiveInfinity, -2.0)));
            TestUtils.AreEqual(double.PositiveInfinity, csum(double3(-2.0, 1.0, double.PositiveInfinity)));

            TestUtils.AreEqual(double.NegativeInfinity, csum(double3(double.NegativeInfinity, 1.0, -2.0)));
            TestUtils.AreEqual(double.NegativeInfinity, csum(double3(-1.0, double.NegativeInfinity, 2.0)));
            TestUtils.AreEqual(double.NegativeInfinity, csum(double3(-2.0, 1.0, double.NegativeInfinity)));
        }

        [TestCompiler]
        public static void csum_double4()
        {
            TestUtils.AreEqual(1.2, csum(double4(2.2, -1.5, 1.2, -0.7)), 4, false);
            TestUtils.AreEqual(1.2e307, csum(double4(2.2e307, -1.5e307, 1.2e307, -0.7e307)), 4, false);
            TestUtils.AreEqual(double.NegativeInfinity, csum(double4(-1.2e308, -0.7e308, -1.4e308, -0.9e308)));
            TestUtils.AreEqual(double.PositiveInfinity, csum(double4(1.2e308, 0.7e308, 1.4e308, 0.9e308)));

            TestUtils.AreEqual(double.NaN, csum(double4(double.NegativeInfinity, double.PositiveInfinity, 100.0, 200.0)));

            TestUtils.AreEqual(double.NaN, csum(double4(double.NaN, 1.0, 1.0, 1.0)));
            TestUtils.AreEqual(double.NaN, csum(double4(1.0, double.NaN, 1.0, 1.0)));
            TestUtils.AreEqual(double.NaN, csum(double4(1.0, 1.0, double.NaN, 1.0)));
            TestUtils.AreEqual(double.NaN, csum(double4(1.0, 1.0, 1.0, double.NaN)));

            TestUtils.AreEqual(double.PositiveInfinity, csum(double4(double.PositiveInfinity, 1.0, -2.0, 3.0)));
            TestUtils.AreEqual(double.PositiveInfinity, csum(double4(1.0, double.PositiveInfinity, -2.0, 3.0)));
            TestUtils.AreEqual(double.PositiveInfinity, csum(double4(1.0, -2.0, double.PositiveInfinity, 3.0)));
            TestUtils.AreEqual(double.PositiveInfinity, csum(double4(1.0, -2.0, 3.0, double.PositiveInfinity)));

            TestUtils.AreEqual(double.NegativeInfinity, csum(double4(double.NegativeInfinity, 1.0, -2.0, 3.0)));
            TestUtils.AreEqual(double.NegativeInfinity, csum(double4(1.0, double.NegativeInfinity, -2.0, 3.0)));
            TestUtils.AreEqual(double.NegativeInfinity, csum(double4(1.0, -2.0, double.NegativeInfinity, 3.0)));
            TestUtils.AreEqual(double.NegativeInfinity, csum(double4(1.0, -2.0, 3.0, double.NegativeInfinity)));
        }


        [TestCompiler]
        public static void any_bool2()
        {
            TestUtils.AreEqual(false, any(bool2(false, false)));
            TestUtils.AreEqual(true, any(bool2(false, true)));
            TestUtils.AreEqual(true, any(bool2(true, false)));
            TestUtils.AreEqual(true, any(bool2(true, true)));
        }

        [TestCompiler]
        public static void any_bool3()
        {
            TestUtils.AreEqual(false, any(bool3(false, false, false)));
            TestUtils.AreEqual(true, any(bool3(false, false, true)));
            TestUtils.AreEqual(true, any(bool3(false, true, false)));
            TestUtils.AreEqual(true, any(bool3(false, true, true)));

            TestUtils.AreEqual(true, any(bool3(true, false, false)));
            TestUtils.AreEqual(true, any(bool3(true, false, true)));
            TestUtils.AreEqual(true, any(bool3(true, true, false)));
            TestUtils.AreEqual(true, any(bool3(true, true, true)));
        }

        [TestCompiler]
        public static void any_bool4()
        {
            TestUtils.AreEqual(false, any(bool4(false, false, false, false)));
            TestUtils.AreEqual(true, any(bool4(false, false, false, true)));
            TestUtils.AreEqual(true, any(bool4(false, false, true, false)));
            TestUtils.AreEqual(true, any(bool4(false, false, true, true)));

            TestUtils.AreEqual(true, any(bool4(false, true, false, false)));
            TestUtils.AreEqual(true, any(bool4(false, true, false, true)));
            TestUtils.AreEqual(true, any(bool4(false, true, true, false)));
            TestUtils.AreEqual(true, any(bool4(false, true, true, true)));

            TestUtils.AreEqual(true, any(bool4(true, false, false, false)));
            TestUtils.AreEqual(true, any(bool4(true, false, false, true)));
            TestUtils.AreEqual(true, any(bool4(true, false, true, false)));
            TestUtils.AreEqual(true, any(bool4(true, false, true, true)));

            TestUtils.AreEqual(true, any(bool4(true, true, false, false)));
            TestUtils.AreEqual(true, any(bool4(true, true, false, true)));
            TestUtils.AreEqual(true, any(bool4(true, true, true, false)));
            TestUtils.AreEqual(true, any(bool4(true, true, true, true)));
        }

        [TestCompiler]
        public static void any_int2()
        {
            TestUtils.AreEqual(false, any(int2(0, 0)));
            TestUtils.AreEqual(true, any(int2(0, -1)));
            TestUtils.AreEqual(true, any(int2(1, 0)));
            TestUtils.AreEqual(true, any(int2(2, int.MinValue)));
        }


        [TestCompiler]
        public static void any_int3()
        {
            TestUtils.AreEqual(false, any(int3(0, 0, 0)));
            TestUtils.AreEqual(true, any(int3(0, 0, 1)));
            TestUtils.AreEqual(true, any(int3(0, -1, 0)));
            TestUtils.AreEqual(true, any(int3(0, int.MinValue, int.MaxValue)));

            TestUtils.AreEqual(true, any(int3(5, 0, 0)));
            TestUtils.AreEqual(true, any(int3(11, 0, -11)));
            TestUtils.AreEqual(true, any(int3(-100, -32, 0)));
            TestUtils.AreEqual(true, any(int3(-121, 100, 322)));
        }

        [TestCompiler]
        public static void any_int4()
        {
            TestUtils.AreEqual(false, any(int4(0, 0, 0, 0)));
            TestUtils.AreEqual(true, any(int4(0, 0, 0, 1)));
            TestUtils.AreEqual(true, any(int4(0, 0, -1, 0)));
            TestUtils.AreEqual(true, any(int4(0, 0, int.MinValue, int.MaxValue)));

            TestUtils.AreEqual(true, any(int4(0, 5, 0, 0)));
            TestUtils.AreEqual(true, any(int4(0, 11, 0, -11)));
            TestUtils.AreEqual(true, any(int4(0, 12, 55, 0)));
            TestUtils.AreEqual(true, any(int4(0, 100, 102, 10123)));

            TestUtils.AreEqual(true, any(int4(-323, 0, 0, 0)));
            TestUtils.AreEqual(true, any(int4(-564, 0, 0, 55)));
            TestUtils.AreEqual(true, any(int4(23, 0, 12, 0)));
            TestUtils.AreEqual(true, any(int4(100, 0, 55, 22)));

            TestUtils.AreEqual(true, any(int4(22, -99, 0, 0)));
            TestUtils.AreEqual(true, any(int4(33, -88, 0, 100)));
            TestUtils.AreEqual(true, any(int4(44, -77, 0x10000, 0)));
            TestUtils.AreEqual(true, any(int4(55, -66, 0x20000, 0x30000)));
        }


        [TestCompiler]
        public static void any_uint2()
        {
            TestUtils.AreEqual(false, any(uint2(0u, 0u)));
            TestUtils.AreEqual(true, any(uint2(0u, 0xFFFFFFFFu)));
            TestUtils.AreEqual(true, any(uint2(100u, 0u)));
            TestUtils.AreEqual(true, any(uint2(200u, 1000u)));
        }


        [TestCompiler]
        public static void any_uint3()
        {
            TestUtils.AreEqual(false, any(uint3(0u, 0u, 0u)));
            TestUtils.AreEqual(true, any(uint3(0u, 0u, 1u)));
            TestUtils.AreEqual(true, any(uint3(0u, 0xFFFFFFFFu, 0u)));
            TestUtils.AreEqual(true, any(uint3(0u, uint.MinValue, uint.MaxValue)));

            TestUtils.AreEqual(true, any(uint3(5u, 0u, 0u)));
            TestUtils.AreEqual(true, any(uint3(11u, 0u, 100u)));
            TestUtils.AreEqual(true, any(uint3(100u, 32u, 0u)));
            TestUtils.AreEqual(true, any(uint3(121u, 100u, 322u)));
        }

        [TestCompiler]
        public static void any_uint4()
        {
            TestUtils.AreEqual(false, any(uint4(0u, 0u, 0u, 0u)));
            TestUtils.AreEqual(true, any(uint4(0u, 0u, 0u, 1u)));
            TestUtils.AreEqual(true, any(uint4(0u, 0u, 0xFFFFFFFFu, 0u)));
            TestUtils.AreEqual(true, any(uint4(0u, 0u, uint.MinValue, uint.MaxValue)));

            TestUtils.AreEqual(true, any(uint4(0u, 5u, 0u, 0u)));
            TestUtils.AreEqual(true, any(uint4(0u, 11u, 0u, 22u)));
            TestUtils.AreEqual(true, any(uint4(0u, 12u, 55u, 0u)));
            TestUtils.AreEqual(true, any(uint4(0u, 100u, 102u, 10123u)));

            TestUtils.AreEqual(true, any(uint4(323u, 0u, 0u, 0u)));
            TestUtils.AreEqual(true, any(uint4(564u, 0u, 0u, 55u)));
            TestUtils.AreEqual(true, any(uint4(23u, 0u, 12u, 0u)));
            TestUtils.AreEqual(true, any(uint4(100u, 0u, 55u, 22u)));

            TestUtils.AreEqual(true, any(uint4(22u, 99u, 0u, 0u)));
            TestUtils.AreEqual(true, any(uint4(33u, 88u, 0u, 100u)));
            TestUtils.AreEqual(true, any(uint4(44u, 77u, 0x10000, 0u)));
            TestUtils.AreEqual(true, any(uint4(55u, 66u, 0x20000u, 0x30000u)));
        }


        [TestCompiler]
        public static void any_float2()
        {
            //TestUtils.AreEqual(true, any(float2(0, float.NaN)));    // TODO: doesn't work with burst

            TestUtils.AreEqual(false, any(float2(0, -0)));
            //TestUtils.AreEqual(true, any(float2(0, float.NaN)));    // TODO: doesn't work with burst
            TestUtils.AreEqual(true, any(float2(-2.0f, 0)));
            TestUtils.AreEqual(true, any(float2(float.PositiveInfinity, float.NegativeInfinity)));
        }


        [TestCompiler]
        public static void any_float3()
        {
            //TestUtils.AreEqual(true, any(float3(0.0f, float.NaN, 0.0f)));    // TODO: doesn't work with burst

            TestUtils.AreEqual(false, any(float3(0.0f, 0.0f, 0.0f)));
            //TestUtils.AreEqual(true, any(float3(0.0f, 0.0f, float.NaN)));    // TODO: doesn't work with burst
            TestUtils.AreEqual(true, any(float3(0.0f, -1.0f, 0.0f)));
            TestUtils.AreEqual(true, any(float3(0.0f, float.NegativeInfinity, float.PositiveInfinity)));

            TestUtils.AreEqual(true, any(float3(float.PositiveInfinity, 0.0f, 0.0f)));
            TestUtils.AreEqual(true, any(float3(float.MaxValue, -0.0f, 1e38f)));
            TestUtils.AreEqual(true, any(float3(-1.2e28f, 3.2f, 0.0f)));
            TestUtils.AreEqual(true, any(float3(121.2f, 100.0f, -32.2f)));
        }

        [TestCompiler]
        public static void any_float4()
        {
            TestUtils.AreEqual(false, any(float4(0.0f, 0.0f, 0.0f, 0.0f)));
            //TestUtils.AreEqual(true, any(float4(0.0f, 0.0f, 0.0f, float.NaN)));    // TODO: doesn't work with burst
            TestUtils.AreEqual(true, any(float4(0.0f, 0.0f, 1.0f, 0.0f)));
            TestUtils.AreEqual(true, any(float4(0.0f, 0.0f, float.NegativeInfinity, float.PositiveInfinity)));

            TestUtils.AreEqual(true, any(float4(0.0f, float.PositiveInfinity, 0.0f, 0.0f)));
            TestUtils.AreEqual(true, any(float4(0.0f, 11.2f, 0.0f, float.MinValue)));
            TestUtils.AreEqual(true, any(float4(0.0f, -12.2f, float.MaxValue, 0u)));
            TestUtils.AreEqual(true, any(float4(0.0f, -1.2e28f, -32.2f, 22.0f)));

            TestUtils.AreEqual(true, any(float4(323.2f, 0.0f, 0.0f, 0.0f)));
            TestUtils.AreEqual(true, any(float4(-564.6f, 0.0f, 0.0f, 1113f)));
            TestUtils.AreEqual(true, any(float4(-23.0f, 0.0f, 0.2f, 0.0f)));
            TestUtils.AreEqual(true, any(float4(102.0f, 0.0f, 5.5f, -22.0f)));

            //TestUtils.AreEqual(true, any(float4(float.NaN, -99.0f, 0.0f, 0.0f)));    // TODO: doesn't work with burst
            TestUtils.AreEqual(true, any(float4(33.0f, 88.0f, 0.0f, 100.0f)));
            TestUtils.AreEqual(true, any(float4(44.0f, 77.0f, -2000.0f, 0.0f)));
            TestUtils.AreEqual(true, any(float4(55.0f, 66.0f, 5000.0f, 10000.2f)));
        }

        [TestCompiler]
        public static void any_double2()
        {
            //TestUtils.AreEqual(true, any(double2(0, double.NaN)));    // TODO: doesn't work with burst.

            TestUtils.AreEqual(false, any(double2(0, -0)));
            //TestUtils.AreEqual(true, any(double2(0, double.NaN)));    // TODO: doesn't work with burst
            TestUtils.AreEqual(true, any(double2(-2.0, 0)));
            TestUtils.AreEqual(true, any(double2(double.PositiveInfinity, double.NegativeInfinity)));
        }


        [TestCompiler]
        public static void any_double3()
        {
            //TestUtils.AreEqual(true, any(double3(0.0, double.NaN, 0.0)));    // TODO: doesn't work with burst

            TestUtils.AreEqual(false, any(double3(0.0, 0.0, 0.0)));
            //TestUtils.AreEqual(true, any(double3(0.0, 0.0, double.NaN)));    // TODO: doesn't work with burst
            TestUtils.AreEqual(true, any(double3(0.0, -1.0, 0.0)));
            TestUtils.AreEqual(true, any(double3(0.0, double.NegativeInfinity, double.PositiveInfinity)));

            TestUtils.AreEqual(true, any(double3(double.PositiveInfinity, 0.0, 0.0)));
            TestUtils.AreEqual(true, any(double3(double.MaxValue, -0.0, 1e108)));
            TestUtils.AreEqual(true, any(double3(-1.2e128, 3.2, 0.0)));
            TestUtils.AreEqual(true, any(double3(121.2, 100.0, -32.2)));
        }

        [TestCompiler]
        public static void any_double4()
        {
            TestUtils.AreEqual(false, any(double4(0.0, 0.0, 0.0, 0.0)));
            //TestUtils.AreEqual(true, any(double4(0.0, 0.0, 0.0, double.NaN)));    // TODO: doesn't work with burst
            TestUtils.AreEqual(true, any(double4(0.0, 0.0, 1.0, 0.0)));
            TestUtils.AreEqual(true, any(double4(0.0, 0.0, double.NegativeInfinity, double.PositiveInfinity)));

            TestUtils.AreEqual(true, any(double4(0.0, double.PositiveInfinity, 0.0, 0.0)));
            TestUtils.AreEqual(true, any(double4(0.0, 11.2, 0.0, double.MinValue)));
            TestUtils.AreEqual(true, any(double4(0.0, -12.2, double.MaxValue, 0.0)));
            TestUtils.AreEqual(true, any(double4(0.0, -1.2e28, -32.2, 22.0)));

            TestUtils.AreEqual(true, any(double4(323.2, 0.0, 0.0, 0.0)));
            TestUtils.AreEqual(true, any(double4(-564.6, 0.0, 0.0, 1113.0)));
            TestUtils.AreEqual(true, any(double4(-23.0, 0.0, 0.2, 0.0)));
            TestUtils.AreEqual(true, any(double4(102.0, 0.0, 5.5, -22.0)));

            //TestUtils.AreEqual(true, any(double4(double.NaN, -99.0, 0.0, 0.0)));    // TODO: doesn't work with burst
            TestUtils.AreEqual(true, any(double4(33.0, 88.0, 0.0, 100.0)));
            TestUtils.AreEqual(true, any(double4(44.0, 77.0, -2000.0, 0.0)));
            TestUtils.AreEqual(true, any(double4(55.0, 66.0, 5000.0, 10000.2)));
        }


        [TestCompiler]
        public static void all_bool2()
        {
            TestUtils.AreEqual(false, all(bool2(false, false)));
            TestUtils.AreEqual(false, all(bool2(false, true)));
            TestUtils.AreEqual(false, all(bool2(true, false)));
            TestUtils.AreEqual(true, all(bool2(true, true)));
        }

        [TestCompiler]
        public static void all_bool3()
        {
            TestUtils.AreEqual(false, all(bool3(false, false, false)));
            TestUtils.AreEqual(false, all(bool3(false, false, true)));
            TestUtils.AreEqual(false, all(bool3(false, true, false)));
            TestUtils.AreEqual(false, all(bool3(false, true, true)));

            TestUtils.AreEqual(false, all(bool3(true, false, false)));
            TestUtils.AreEqual(false, all(bool3(true, false, true)));
            TestUtils.AreEqual(false, all(bool3(true, true, false)));
            TestUtils.AreEqual(true, all(bool3(true, true, true)));
        }

        [TestCompiler]
        public static void all_bool4()
        {
            TestUtils.AreEqual(false, all(bool4(false, false, false, false)));
            TestUtils.AreEqual(false, all(bool4(false, false, false, true)));
            TestUtils.AreEqual(false, all(bool4(false, false, true, false)));
            TestUtils.AreEqual(false, all(bool4(false, false, true, true)));

            TestUtils.AreEqual(false, all(bool4(false, true, false, false)));
            TestUtils.AreEqual(false, all(bool4(false, true, false, true)));
            TestUtils.AreEqual(false, all(bool4(false, true, true, false)));
            TestUtils.AreEqual(false, all(bool4(false, true, true, true)));

            TestUtils.AreEqual(false, all(bool4(true, false, false, false)));
            TestUtils.AreEqual(false, all(bool4(true, false, false, true)));
            TestUtils.AreEqual(false, all(bool4(true, false, true, false)));
            TestUtils.AreEqual(false, all(bool4(true, false, true, true)));

            TestUtils.AreEqual(false, all(bool4(true, true, false, false)));
            TestUtils.AreEqual(false, all(bool4(true, true, false, true)));
            TestUtils.AreEqual(false, all(bool4(true, true, true, false)));
            TestUtils.AreEqual(true, all(bool4(true, true, true, true)));
        }

        [TestCompiler]
        public static void all_int2()
        {
            TestUtils.AreEqual(false, all(int2(0, 0)));
            TestUtils.AreEqual(false, all(int2(0, -1)));
            TestUtils.AreEqual(false, all(int2(1, 0)));
            TestUtils.AreEqual(true, all(int2(2, int.MinValue)));
        }

        [TestCompiler]
        public static void all_int3()
        {
            TestUtils.AreEqual(false, all(int3(0, 0, 0)));
            TestUtils.AreEqual(false, all(int3(0, 0, 1)));
            TestUtils.AreEqual(false, all(int3(0, -1, 0)));
            TestUtils.AreEqual(false, all(int3(0, int.MinValue, int.MaxValue)));

            TestUtils.AreEqual(false, all(int3(5, 0, 0)));
            TestUtils.AreEqual(false, all(int3(11, 0, -11)));
            TestUtils.AreEqual(false, all(int3(-100, -32, 0)));
            TestUtils.AreEqual(true, all(int3(-121, 100, 322)));
        }

        [TestCompiler]
        public static void all_int4()
        {
            TestUtils.AreEqual(false, all(int4(0, 0, 0, 0)));
            TestUtils.AreEqual(false, all(int4(0, 0, 0, 1)));
            TestUtils.AreEqual(false, all(int4(0, 0, -1, 0)));
            TestUtils.AreEqual(false, all(int4(0, 0, int.MinValue, int.MaxValue)));

            TestUtils.AreEqual(false, all(int4(0, 5, 0, 0)));
            TestUtils.AreEqual(false, all(int4(0, 11, 0, -11)));
            TestUtils.AreEqual(false, all(int4(0, 12, 55, 0)));
            TestUtils.AreEqual(false, all(int4(0, 100, 102, 10123)));

            TestUtils.AreEqual(false, all(int4(-323, 0, 0, 0)));
            TestUtils.AreEqual(false, all(int4(-564, 0, 0, 55)));
            TestUtils.AreEqual(false, all(int4(23, 0, 12, 0)));
            TestUtils.AreEqual(false, all(int4(100, 0, 55, 22)));

            TestUtils.AreEqual(false, all(int4(22, -99, 0, 0)));
            TestUtils.AreEqual(false, all(int4(33, -88, 0, 100)));
            TestUtils.AreEqual(false, all(int4(44, -77, 0x10000, 0)));
            TestUtils.AreEqual(true, all(int4(55, -66, 0x20000, 0x30000)));
        }


        [TestCompiler]
        public static void all_uint2()
        {
            TestUtils.AreEqual(false, all(uint2(0u, 0u)));
            TestUtils.AreEqual(false, all(uint2(0u, 0xFFFFFFFFu)));
            TestUtils.AreEqual(false, all(uint2(100u, 0u)));
            TestUtils.AreEqual(true, all(uint2(200u, 1000u)));
        }


        [TestCompiler]
        public static void all_uint3()
        {
            TestUtils.AreEqual(false, all(uint3(0u, 0u, 0u)));
            TestUtils.AreEqual(false, all(uint3(0u, 0u, 1u)));
            TestUtils.AreEqual(false, all(uint3(0u, 0xFFFFFFFFu, 0u)));
            TestUtils.AreEqual(false, all(uint3(0u, uint.MinValue, uint.MaxValue)));

            TestUtils.AreEqual(false, all(uint3(5u, 0u, 0u)));
            TestUtils.AreEqual(false, all(uint3(11u, 0u, 100u)));
            TestUtils.AreEqual(false, all(uint3(100u, 32u, 0u)));
            TestUtils.AreEqual(true, all(uint3(121u, 100u, 322u)));
        }

        [TestCompiler]
        public static void all_uint4()
        {
            TestUtils.AreEqual(false, all(uint4(0u, 0u, 0u, 0u)));
            TestUtils.AreEqual(false, all(uint4(0u, 0u, 0u, 1u)));
            TestUtils.AreEqual(false, all(uint4(0u, 0u, 0xFFFFFFFFu, 0u)));
            TestUtils.AreEqual(false, all(uint4(0u, 0u, uint.MinValue, uint.MaxValue)));

            TestUtils.AreEqual(false, all(uint4(0u, 5u, 0u, 0u)));
            TestUtils.AreEqual(false, all(uint4(0u, 11u, 0u, 22u)));
            TestUtils.AreEqual(false, all(uint4(0u, 12u, 55u, 0u)));
            TestUtils.AreEqual(false, all(uint4(0u, 100u, 102u, 10123u)));

            TestUtils.AreEqual(false, all(uint4(323u, 0u, 0u, 0u)));
            TestUtils.AreEqual(false, all(uint4(564u, 0u, 0u, 55u)));
            TestUtils.AreEqual(false, all(uint4(23u, 0u, 12u, 0u)));
            TestUtils.AreEqual(false, all(uint4(100u, 0u, 55u, 22u)));

            TestUtils.AreEqual(false, all(uint4(22u, 99u, 0u, 0u)));
            TestUtils.AreEqual(false, all(uint4(33u, 88u, 0u, 100u)));
            TestUtils.AreEqual(false, all(uint4(44u, 77u, 0x10000, 0u)));
            TestUtils.AreEqual(true, all(uint4(55u, 66u, 0x20000u, 0x30000u)));
        }


        [TestCompiler]
        public static void all_float2()
        {
            TestUtils.AreEqual(true, all(float2(float.NaN, float.NaN)));

            TestUtils.AreEqual(false, all(float2(0, -0)));
            TestUtils.AreEqual(false, all(float2(0, float.NaN)));
            TestUtils.AreEqual(false, all(float2(-2.0f, 0)));
            TestUtils.AreEqual(true, all(float2(float.PositiveInfinity, float.NegativeInfinity)));
        }


        [TestCompiler]
        public static void all_float3()
        {
            TestUtils.AreEqual(true, all(float3(float.NaN, float.NaN, float.NaN)));

            TestUtils.AreEqual(false, all(float3(0.0f, 0.0f, 0.0f)));
            TestUtils.AreEqual(false, all(float3(0.0f, 0.0f, float.NaN)));
            TestUtils.AreEqual(false, all(float3(0.0f, -1.0f, 0.0f)));
            TestUtils.AreEqual(false, all(float3(0.0f, float.NegativeInfinity, float.PositiveInfinity)));

            TestUtils.AreEqual(false, all(float3(float.PositiveInfinity, 0.0f, 0.0f)));
            TestUtils.AreEqual(false, all(float3(float.MaxValue, -0.0f, 1e38f)));
            TestUtils.AreEqual(false, all(float3(-1.2e28f, 3.2f, 0.0f)));
            TestUtils.AreEqual(true, all(float3(121.2f, 100.0f, -32.2f)));
        }

        [TestCompiler]
        public static void all_float4()
        {
            TestUtils.AreEqual(true, all(float4(float.NaN, float.NaN, float.NaN, float.NaN)));

            TestUtils.AreEqual(false, all(float4(0.0f, 0.0f, 0.0f, 0.0f)));
            TestUtils.AreEqual(false, all(float4(0.0f, 0.0f, 0.0f, float.NaN)));
            TestUtils.AreEqual(false, all(float4(0.0f, 0.0f, 1.0f, 0.0f)));
            TestUtils.AreEqual(false, all(float4(0.0f, 0.0f, float.NegativeInfinity, float.PositiveInfinity)));

            TestUtils.AreEqual(false, all(float4(0.0f, float.PositiveInfinity, 0.0f, 0.0f)));
            TestUtils.AreEqual(false, all(float4(0.0f, 11.2f, 0.0f, float.MinValue)));
            TestUtils.AreEqual(false, all(float4(0.0f, -12.2f, float.MaxValue, 0u)));
            TestUtils.AreEqual(false, all(float4(0.0f, -1.2e28f, -32.2f, 22.0f)));

            TestUtils.AreEqual(false, all(float4(323.2f, 0.0f, 0.0f, 0.0f)));
            TestUtils.AreEqual(false, all(float4(-564.6f, 0.0f, 0.0f, 1113f)));
            TestUtils.AreEqual(false, all(float4(-23.0f, 0.0f, 0.2f, 0.0f)));
            TestUtils.AreEqual(false, all(float4(102.0f, 0.0f, 5.5f, -22.0f)));

            TestUtils.AreEqual(false, all(float4(float.NaN, -99.0f, 0.0f, 0.0f)));
            TestUtils.AreEqual(false, all(float4(33.0f, 88.0f, 0.0f, 100.0f)));
            TestUtils.AreEqual(false, all(float4(44.0f, 77.0f, -2000.0f, 0.0f)));
            TestUtils.AreEqual(true, all(float4(55.0f, 66.0f, 5000.0f, 10000.2f)));
        }

        [TestCompiler]
        public static void all_double2()
        {
            TestUtils.AreEqual(true, all(double2(double.NaN, double.NaN)));

            TestUtils.AreEqual(false, all(double2(0, -0)));
            TestUtils.AreEqual(false, all(double2(0, double.NaN)));
            TestUtils.AreEqual(false, all(double2(-2.0, 0)));
            TestUtils.AreEqual(true, all(double2(double.PositiveInfinity, double.NegativeInfinity)));
        }


        [TestCompiler]
        public static void all_double3()
        {
            TestUtils.AreEqual(true, all(double3(double.NaN, double.NaN, double.NaN)));

            TestUtils.AreEqual(false, all(double3(0.0, 0.0, 0.0)));
            TestUtils.AreEqual(false, all(double3(0.0, 0.0, double.NaN)));
            TestUtils.AreEqual(false, all(double3(0.0, -1.0, 0.0)));
            TestUtils.AreEqual(false, all(double3(0.0, double.NegativeInfinity, double.PositiveInfinity)));

            TestUtils.AreEqual(false, all(double3(double.PositiveInfinity, 0.0, 0.0)));
            TestUtils.AreEqual(false, all(double3(double.MaxValue, -0.0, 1e108)));
            TestUtils.AreEqual(false, all(double3(-1.2e128, 3.2, 0.0)));
            TestUtils.AreEqual(true, all(double3(121.2, 100.0, -32.2)));
        }

        [TestCompiler]
        public static void all_double4()
        {
            TestUtils.AreEqual(true, all(double4(double.NaN, double.NaN, double.NaN, double.NaN)));

            TestUtils.AreEqual(false, all(double4(0.0, 0.0, 0.0, 0.0)));
            TestUtils.AreEqual(false, all(double4(0.0, 0.0, 0.0, double.NaN)));
            TestUtils.AreEqual(false, all(double4(0.0, 0.0, 1.0, 0.0)));
            TestUtils.AreEqual(false, all(double4(0.0, 0.0, double.NegativeInfinity, double.PositiveInfinity)));

            TestUtils.AreEqual(false, all(double4(0.0, double.PositiveInfinity, 0.0, 0.0)));
            TestUtils.AreEqual(false, all(double4(0.0, 11.2, 0.0, double.MinValue)));
            TestUtils.AreEqual(false, all(double4(0.0, -12.2, double.MaxValue, 0.0)));
            TestUtils.AreEqual(false, all(double4(0.0, -1.2e28, -32.2, 22.0)));

            TestUtils.AreEqual(false, all(double4(323.2, 0.0, 0.0, 0.0)));
            TestUtils.AreEqual(false, all(double4(-564.6, 0.0, 0.0, 1113.0)));
            TestUtils.AreEqual(false, all(double4(-23.0, 0.0, 0.2, 0.0)));
            TestUtils.AreEqual(false, all(double4(102.0, 0.0, 5.5, -22.0)));

            TestUtils.AreEqual(false, all(double4(double.NaN, -99.0, 0.0, 0.0)));
            TestUtils.AreEqual(false, all(double4(33.0, 88.0, 0.0, 100.0)));
            TestUtils.AreEqual(false, all(double4(44.0, 77.0, -2000.0, 0.0)));
            TestUtils.AreEqual(true, all(double4(55.0, 66.0, 5000.0, 10000.2)));
        }


        [TestCompiler]
        public static void length_float2()
        {
            TestUtils.AreEqual(0.0f, length(float2(0.0f, 0.0f)), 0, false);
            TestUtils.AreEqual(2.86356421265527063f, length(float2(1.2f, -2.6f)), 8, false);
            TestUtils.AreEqual(float.NaN, length(float2(1.2f, float.NaN)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, length(float2(1.2f, float.PositiveInfinity)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, length(float2(1.2f, float.NegativeInfinity)), 0, false);

            TestUtils.AreEqual(2.863564e18f, length(float2(-1.2e18f, 2.6e18f)), 8, false);
            TestUtils.AreEqual(double.PositiveInfinity, length(float2(-1.2e19f, -2.6e19f)), 8, false);
        }

        [TestCompiler]
        public static void length_float3()
        {
            TestUtils.AreEqual(0.0f, length(float3(0.0f, 0.0f, 0.0f)), 0, false);
            TestUtils.AreEqual(3.611094f, length(float3(1.2f, -2.6f, 2.2f)), 8, false);
            TestUtils.AreEqual(float.NaN, length(float3(1.2f, float.NaN, 2.2f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, length(float3(1.2f, float.PositiveInfinity, 2.2f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, length(float3(1.2f, float.NegativeInfinity, 2.2f)), 0, false);

            TestUtils.AreEqual(3.611094e18f, length(float3(-1.2e18f, 2.6e18f, 2.2e18f)), 8, false);
            TestUtils.AreEqual(float.PositiveInfinity, length(float3(-1.2e19f, -2.6e19f, 2.2e19f)), 8, false);
        }

        [TestCompiler]
        public static void length_float4()
        {
            TestUtils.AreEqual(0.0f, length(float4(0.0f, 0.0f, 0.0f, 0.0f)), 0, false);
            TestUtils.AreEqual(5.538953f, length(float4(1.2f, -2.6f, 2.2f, -4.2f)), 8, false);
            TestUtils.AreEqual(float.NaN, length(float4(1.2f, float.NaN, 2.2f, -4.2f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, length(float4(1.2f, float.PositiveInfinity, 2.2f, -4.2f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, length(float4(1.2f, float.NegativeInfinity, 2.2f, -4.2f)), 0, false);

            TestUtils.AreEqual(5.538953e18f, length(float4(-1.2e18f, 2.6e18f, 2.2e18f, -4.2e18f)), 8, false);
            TestUtils.AreEqual(float.PositiveInfinity, length(float4(-1.2e19f, -2.6e19f, 2.2e19f, -4.2e19f)), 8, false);
        }


        [TestCompiler]
        public static void length_double2()
        {
            TestUtils.AreEqual(0.0, length(double2(0.0, 0.0)), 0, false);
            TestUtils.AreEqual(2.86356421265527063, length(double2(1.2, -2.6)), 8, false);
            TestUtils.AreEqual(double.NaN, length(double2(1.2, double.NaN)), 0, false);
            TestUtils.AreEqual(double.PositiveInfinity, length(double2(1.2, double.PositiveInfinity)), 0, false);
            TestUtils.AreEqual(double.PositiveInfinity, length(double2(1.2, double.NegativeInfinity)), 0, false);

            TestUtils.AreEqual(2.86356421265527063e153, length(double2(-1.2e153, 2.6e153)), 8, false);
        }

        [TestCompiler]
        public static void length_double3()
        {
            TestUtils.AreEqual(0.0, length(double3(0.0, 0.0, 0.0)), 0, false);
            TestUtils.AreEqual(3.6110940170535577, length(double3(1.2, -2.6, 2.2)), 8, false);
            TestUtils.AreEqual(double.NaN, length(double3(1.2, double.NaN, 2.2)), 0, false);
            TestUtils.AreEqual(double.PositiveInfinity, length(double3(1.2, double.PositiveInfinity, 2.2)), 0, false);
            TestUtils.AreEqual(double.PositiveInfinity, length(double3(1.2, double.NegativeInfinity, 2.2)), 0, false);

            TestUtils.AreEqual(3.6110940170535577e153, length(double3(-1.2e153, 2.6e153, 2.2e153)), 8, false);
        }

        [TestCompiler]
        public static void length_double4()
        {
            TestUtils.AreEqual(0.0, length(double4(0.0, 0.0, 0.0, 0.0)), 0, false);
            TestUtils.AreEqual(5.5389529696504916, length(double4(1.2, -2.6, 2.2, -4.2)), 8, false);
            TestUtils.AreEqual(double.NaN, length(double4(1.2, double.NaN, 2.2, -4.2)), 0, false);
            TestUtils.AreEqual(double.PositiveInfinity, length(double4(1.2, double.PositiveInfinity, 2.2, -4.2)), 0, false);
            TestUtils.AreEqual(double.PositiveInfinity, length(double4(1.2, double.NegativeInfinity, 2.2, -4.2)), 0, false);

            TestUtils.AreEqual(5.5389529696504916e153, length(double4(-1.2e153, 2.6e153, 2.2e153, -4.2e153)), 8, false);
        }


        [TestCompiler]
        public static void lengthsq_float2()
        {
            TestUtils.AreEqual(0.0f, lengthsq(float2(0.0f, 0.0f)), 0, false);
            TestUtils.AreEqual(8.2f, lengthsq(float2(1.2f, -2.6f)), 8, false);
            TestUtils.AreEqual(float.NaN, lengthsq(float2(1.2f, float.NaN)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, lengthsq(float2(1.2f, float.PositiveInfinity)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, lengthsq(float2(1.2f, float.NegativeInfinity)), 0, false);

            TestUtils.AreEqual(8.2e36f, lengthsq(float2(-1.2e18f, 2.6e18f)), 8, false);
        }

        [TestCompiler]
        public static void lengthsq_float3()
        {
            TestUtils.AreEqual(0.0f, lengthsq(float3(0.0f, 0.0f, 0.0f)), 0, false);
            TestUtils.AreEqual(13.04f, lengthsq(float3(1.2f, -2.6f, 2.2f)), 8, false);
            TestUtils.AreEqual(float.NaN, lengthsq(float3(1.2f, float.NaN, 2.2f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, lengthsq(float3(1.2f, float.PositiveInfinity, 2.2f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, lengthsq(float3(1.2f, float.NegativeInfinity, 2.2f)), 0, false);

            TestUtils.AreEqual(1.304e37f, lengthsq(float3(-1.2e18f, 2.6e18f, 2.2e18f)), 8, false);
            TestUtils.AreEqual(float.PositiveInfinity, lengthsq(float3(-1.2e19f, -2.6e19f, 2.2e19f)), 8, false);
        }

        [TestCompiler]
        public static void lengthsq_float4()
        {
            TestUtils.AreEqual(0.0f, lengthsq(float4(0.0f, 0.0f, 0.0f, 0.0f)), 0, false);
            TestUtils.AreEqual(30.68f, lengthsq(float4(1.2f, -2.6f, 2.2f, -4.2f)), 8, false);
            TestUtils.AreEqual(float.NaN, lengthsq(float4(1.2f, float.NaN, 2.2f, -4.2f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, lengthsq(float4(1.2f, float.PositiveInfinity, 2.2f, -4.2f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, lengthsq(float4(1.2f, float.NegativeInfinity, 2.2f, -4.2f)), 0, false);

            TestUtils.AreEqual(3.068e37f, lengthsq(float4(-1.2e18f, 2.6e18f, 2.2e18f, -4.2e18f)), 8, false);
            TestUtils.AreEqual(float.PositiveInfinity, lengthsq(float4(-1.2e19f, -2.6e19f, 2.2e19f, -4.2e19f)), 8, false);
        }


        [TestCompiler]
        public static void lengthsq_double2()
        {
            TestUtils.AreEqual(0.0, lengthsq(double2(0.0, 0.0)), 0, false);
            TestUtils.AreEqual(8.2, lengthsq(double2(1.2, -2.6)), 8, false);
            TestUtils.AreEqual(double.NaN, lengthsq(double2(1.2, double.NaN)), 0, false);
            TestUtils.AreEqual(double.PositiveInfinity, lengthsq(double2(1.2, double.PositiveInfinity)), 0, false);
            TestUtils.AreEqual(double.PositiveInfinity, lengthsq(double2(1.2, double.NegativeInfinity)), 0, false);

            TestUtils.AreEqual(8.2e306, lengthsq(double2(-1.2e153, 2.6e153)), 8, false);
            TestUtils.AreEqual(double.PositiveInfinity, lengthsq(double2(-1.2e154, -2.6e154)), 8, false);
        }

        [TestCompiler]
        public static void lengthsq_double3()
        {
            TestUtils.AreEqual(0.0, lengthsq(double3(0.0, 0.0, 0.0)), 0, false);
            TestUtils.AreEqual(13.04, lengthsq(double3(1.2, -2.6, 2.2)), 8, false);
            TestUtils.AreEqual(double.NaN, lengthsq(double3(1.2, double.NaN, 2.2)), 0, false);
            TestUtils.AreEqual(double.PositiveInfinity, lengthsq(double3(1.2, double.PositiveInfinity, 2.2)), 0, false);
            TestUtils.AreEqual(double.PositiveInfinity, lengthsq(double3(1.2, double.NegativeInfinity, 2.2)), 0, false);

            TestUtils.AreEqual(1.304e307, lengthsq(double3(-1.2e153, 2.6e153, 2.2e153)), 8, false);
            TestUtils.AreEqual(double.PositiveInfinity, lengthsq(double3(-1.2e154, -2.6e154, 2.2e154)), 8, false);
        }

        [TestCompiler]
        public static void lengthsq_double4()
        {
            TestUtils.AreEqual(0.0, lengthsq(double4(0.0, 0.0, 0.0, 0.0)), 0, false);
            TestUtils.AreEqual(30.68, lengthsq(double4(1.2, -2.6, 2.2, -4.2)), 8, false);
            TestUtils.AreEqual(double.NaN, lengthsq(double4(1.2, double.NaN, 2.2, -4.2)), 0, false);
            TestUtils.AreEqual(double.PositiveInfinity, lengthsq(double4(1.2, double.PositiveInfinity, 2.2, -4.2)), 0, false);
            TestUtils.AreEqual(double.PositiveInfinity, lengthsq(double4(1.2, double.NegativeInfinity, 2.2, -4.2)), 0, false);

            TestUtils.AreEqual(3.068e307, lengthsq(double4(-1.2e153, 2.6e153, 2.2e153, -4.2e153)), 8, false);
            TestUtils.AreEqual(double.PositiveInfinity, lengthsq(double4(-1.2e154, -2.6e154, 2.2e154, -4.2e154)), 0, false);
        }




        [TestCompiler]
        public static void distance_float2()
        {
            TestUtils.AreEqual(0.0f, distance(float2(1.3f, -2.4f), float2(1.3f, -2.4f)), 0, false);

            TestUtils.AreEqual(9.404786f, distance(float2(1.3f, -2.4f), float2(-5.3f, 4.3f)), 8, false);
            TestUtils.AreEqual(9.404786e18f, distance(float2(1.3e18f, -2.4e18f), float2(-5.3e18f, 4.3e18f)), 8, false);
            TestUtils.AreEqual(float.PositiveInfinity, distance(float2(1.3e19f, -2.4e19f), float2(-5.3e19f, 4.3e19f)), 0, false);

            TestUtils.AreEqual(float.NaN, distance(float2(1.3f, -2.4f), float2(float.NaN, 4.3f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, distance(float2(1.3f, -2.4f), float2(-5.3f, float.PositiveInfinity)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, distance(float2(1.3f, float.NegativeInfinity), float2(-5.3f, 4.3f)), 0, false);
        }

        [TestCompiler]
        public static void distance_float3()
        {
            TestUtils.AreEqual(0.0f, distance(float2(1.3f, -2.4f), float2(1.3f, -2.4f)), 0, false);

            TestUtils.AreEqual(9.404786f, distance(float2(1.3f, -2.4f), float2(-5.3f, 4.3f)), 8, false);
            TestUtils.AreEqual(9.404786e18f, distance(float2(1.3e18f, -2.4e18f), float2(-5.3e18f, 4.3e18f)), 8, false);
            TestUtils.AreEqual(float.PositiveInfinity, distance(float2(1.3e19f, -2.4e19f), float2(-5.3e19f, 4.3e19f)), 0, false);

            TestUtils.AreEqual(float.NaN, distance(float2(1.3f, -2.4f), float2(float.NaN, 4.3f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, distance(float2(1.3f, -2.4f), float2(-5.3f, float.PositiveInfinity)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, distance(float2(1.3f, float.NegativeInfinity), float2(-5.3f, 4.3f)), 0, false);
        }

        [TestCompiler]
        public static void distance_float4()
        {
            TestUtils.AreEqual(0.0, distance(float4(1.3f, -2.4f, 5.7f, 3.1f), float4(1.3f, -2.4f, 5.7f, 3.1f)), 0, false);

            TestUtils.AreEqual(9.863569f, distance(float4(1.3f, -2.4f, 5.7f, 3.1f), float4(-5.3f, 4.3f, 4.7f, 0.3f)), 8, false);
            TestUtils.AreEqual(9.863569e18f, distance(float4(1.3e18f, -2.4e18f, 5.7e18f, 3.1e18f), float4(-5.3e18f, 4.3e18f, 4.7e18f, 3e17f)), 8, false);
            TestUtils.AreEqual(float.PositiveInfinity, distance(float4(1.3e19f, -2.4e19f, 5.7e19f, 3.1e19f), float4(-5.3e19f, 4.3e19f, 4.7e19f, 3e18f)), 0, false);

            TestUtils.AreEqual(float.NaN, distance(float4(1.3f, -2.4f, 5.7f, 3.1f), float4(float.NaN, 4.3f, 4.7f, 0.3f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, distance(float4(1.3f, -2.4f, 5.7f, 3.1f), float4(-5.3f, float.PositiveInfinity, 4.7f, 0.3f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, distance(float4(1.3f, float.NegativeInfinity, 5.7f, 3.1f), double4(-5.3f, 4.3f, 4.7f, 0.3f)), 0, false);
        }


        [TestCompiler]
        public static void distance_double2()
        {
            TestUtils.AreEqual(0.0, distance(double2(1.3, -2.4), double2(1.3, -2.4)), 0, false);

            TestUtils.AreEqual(9.4047860156411852, distance(double2(1.3, -2.4), double2(-5.3, 4.3)), 8, false);
            TestUtils.AreEqual(9.4047860156411852e153, distance(double2(1.3e153, -2.4e153), double2(-5.3e153, 4.3e153)), 8, false);
            TestUtils.AreEqual(double.PositiveInfinity, distance(double2(1.3e154, -2.4e154), double2(-5.3e154, 4.3e154)), 8, false);

            TestUtils.AreEqual(double.NaN, distance(double2(1.3, -2.4), double2(double.NaN, 4.3)), 0, false);
            TestUtils.AreEqual(double.PositiveInfinity, distance(double2(1.3, -2.4), double2(-5.3, double.PositiveInfinity)), 0, false);
        }

        [TestCompiler]
        public static void distance_double3()
        {
            TestUtils.AreEqual(0.0, distance(double2(1.3, -2.4), double2(1.3, -2.4)), 0, false);

            TestUtils.AreEqual(9.4047860156411852, distance(double2(1.3, -2.4), double2(-5.3, 4.3)), 8, false);
            TestUtils.AreEqual(9.4047860156411852e153, distance(double2(1.3e153, -2.4e153), double2(-5.3e153, 4.3e153)), 8, false);
            TestUtils.AreEqual(double.PositiveInfinity, distance(double2(1.3e154, -2.4e154), double2(-5.3e154, 4.3e154)), 8, false);

            TestUtils.AreEqual(double.NaN, distance(double2(1.3, -2.4), double2(double.NaN, 4.3)), 0, false);
            TestUtils.AreEqual(double.PositiveInfinity, distance(double2(1.3, -2.4), double2(-5.3, double.PositiveInfinity)), 0, false);
        }

        [TestCompiler]
        public static void distance_double4()
        {
            TestUtils.AreEqual(0.0, distance(double4(1.3, -2.4, 5.7, 3.1), double4(1.3, -2.4, 5.7, 3.1)), 0, false);

            TestUtils.AreEqual(9.8635693336641579, distance(double4(1.3, -2.4, 5.7, 3.1), double4(-5.3, 4.3, 4.7, 0.3)), 8, false);
            TestUtils.AreEqual(9.8635693336641579e153, distance(double4(1.3e153, -2.4e153, 5.7e153, 3.1e153), double4(-5.3e153, 4.3e153, 4.7e153, 3e152)), 8, false);

            TestUtils.AreEqual(double.NaN, distance(double4(1.3, -2.4, 5.7, 3.1), double4(double.NaN, 4.3, 4.7, 0.3)), 0, false);
            TestUtils.AreEqual(double.PositiveInfinity, distance(double4(1.3, -2.4, 5.7, 3.1), double4(-5.3, double.PositiveInfinity, 4.7, 0.3)), 0, false);
        }


        [TestCompiler]
        public static void distancesq_float2()
        {
            TestUtils.AreEqual(0.0f, distancesq(float2(1.3f, -2.4f), float2(1.3f, -2.4f)), 0, false);

            TestUtils.AreEqual(88.45f, distancesq(float2(1.3f, -2.4f), float2(-5.3f, 4.3f)), 8, false);
            TestUtils.AreEqual(8.845e37f, distancesq(float2(1.3e18f, -2.4e18f), float2(-5.3e18f, 4.3e18f)), 8, false);

            TestUtils.AreEqual(float.NaN, distancesq(float2(1.3f, -2.4f), float2(float.NaN, 4.3f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, distancesq(float2(1.3f, -2.4f), float2(-5.3f, float.PositiveInfinity)), 0, false);
        }

        [TestCompiler]
        public static void distancesq_float3()
        {
            TestUtils.AreEqual(0.0f, distancesq(float3(1.3f, -2.4f, 5.7f), float3(1.3f, -2.4f, 5.7f)), 0, false);

            TestUtils.AreEqual(89.45f, distancesq(float3(1.3f, -2.4f, 5.7f), float3(-5.3f, 4.3f, 4.7f)), 8, false);
            TestUtils.AreEqual(8.945e37f, distancesq(float3(1.3e18f, -2.4e18f, 5.7e18f), float3(-5.3e18f, 4.3e18f, 4.7e18f)), 8, false);
            TestUtils.AreEqual(float.PositiveInfinity, distancesq(float3(1.3e19f, -2.4e19f, 5.7e19f), float3(-5.3e19f, 4.3e19f, 4.7e19f)), 8, false);

            TestUtils.AreEqual(float.NaN, distancesq(float3(1.3f, -2.4f, 5.7f), float3(float.NaN, 4.3f, 4.7f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, distancesq(float3(1.3f, -2.4f, 5.7f), float3(-5.3f, float.PositiveInfinity, 4.7f)), 0, false);
        }

        [TestCompiler]
        public static void distancesq_float4()
        {
            TestUtils.AreEqual(0.0f, distancesq(float4(1.3f, -2.4f, 5.7f, 3.1f), float4(1.3f, -2.4f, 5.7f, 3.1f)), 0, false);

            TestUtils.AreEqual(97.29f, distancesq(float4(1.3f, -2.4f, 5.7f, 3.1f), float4(-5.3f, 4.3f, 4.7f, 0.3f)), 8, false);
            TestUtils.AreEqual(9.729e37f, distancesq(float4(1.3e18f, -2.4e18f, 5.7e18f, 3.1e18f), float4(-5.3e18f, 4.3e18f, 4.7e18f, 3e17f)), 8, false);

            TestUtils.AreEqual(float.NaN, distancesq(float4(1.3f, -2.4f, 5.7f, 3.1f), float4(float.NaN, 4.3f, 4.7f, 0.3f)), 0, false);
            TestUtils.AreEqual(float.PositiveInfinity, distancesq(float4(1.3f, -2.4f, 5.7f, 3.1f), float4(-5.3f, float.PositiveInfinity, 4.7f, 0.3f)), 0, false);
        }


        [TestCompiler]
        public static void distancesq_double2()
        {
            TestUtils.AreEqual(0.0, distancesq(double2(1.3, -2.4), double2(1.3, -2.4)), 0, false);

            TestUtils.AreEqual(88.45, distancesq(double2(1.3, -2.4), double2(-5.3, 4.3)), 8, false);
            TestUtils.AreEqual(8.845e307, distancesq(double2(1.3e153, -2.4e153), double2(-5.3e153, 4.3e153)), 8, false);
            TestUtils.AreEqual(double.PositiveInfinity, distancesq(double2(1.3e154, -2.4e154), double2(-5.3e154, 4.3e154)), 8, false);

            TestUtils.AreEqual(double.NaN, distancesq(double2(1.3, -2.4), double2(double.NaN, 4.3)), 0, false);
            TestUtils.AreEqual(double.PositiveInfinity, distancesq(double2(1.3, -2.4), double2(-5.3, double.PositiveInfinity)), 8, false);
            TestUtils.AreEqual(double.PositiveInfinity, distancesq(double2(1.3, double.NegativeInfinity), double2(-5.3, 4.3)), 8, false);
        }

        [TestCompiler]
        public static void distancesq_double3()
        {
            TestUtils.AreEqual(0.0, distancesq(double3(1.3, -2.4, 5.7), double3(1.3, -2.4, 5.7)), 0, false);

            TestUtils.AreEqual(89.45, distancesq(double3(1.3, -2.4, 5.7), double3(-5.3, 4.3, 4.7)), 8, false);
            TestUtils.AreEqual(8.945e307, distancesq(double3(1.3e153, -2.4e153, 5.7e153), double3(-5.3e153, 4.3e153, 4.7e153)), 8, false);
            TestUtils.AreEqual(double.PositiveInfinity, distancesq(double3(1.3e154, -2.4e154, 5.7e154), double3(-5.3e154, 4.3e154, 4.7e154)), 8, false);

            TestUtils.AreEqual(double.NaN, distancesq(double3(1.3, -2.4, 5.7), double3(double.NaN, 4.3, 4.7)), 0, false);
            TestUtils.AreEqual(double.PositiveInfinity, distancesq(double3(1.3, -2.4, 5.7), double3(-5.3, double.PositiveInfinity, 4.7)), 8, false);
            TestUtils.AreEqual(double.PositiveInfinity, distancesq(double3(1.3, double.NegativeInfinity, 5.7), double3(-5.3, 4.3, 4.7)), 8, false);
        }

        [TestCompiler]
        public static void distancesq_double4()
        {
            TestUtils.AreEqual(0.0, distancesq(double4(1.3, -2.4, 5.7, 3.1), double4(1.3, -2.4, 5.7, 3.1)), 0, false);

            TestUtils.AreEqual(97.29, distancesq(double4(1.3, -2.4, 5.7, 3.1), double4(-5.3, 4.3, 4.7, 0.3)), 8, false);
            TestUtils.AreEqual(9.729e307, distancesq(double4(1.3e153, -2.4e153, 5.7e153, 3.1e153), double4(-5.3e153, 4.3e153, 4.7e153, 3e152)), 8, false);
            TestUtils.AreEqual(double.PositiveInfinity, distancesq(double4(1.3e154, -2.4e154, 5.7e154, 3.1e153), double4(-5.3e154, 4.3e154, 4.7e154, 3e153)), 8, false);

            TestUtils.AreEqual(double.NaN, distancesq(double4(1.3, -2.4, 5.7, 3.1), double4(double.NaN, 4.3, 4.7, 0.3)), 0, false);
            TestUtils.AreEqual(double.PositiveInfinity, distancesq(double4(1.3, -2.4, 5.7, 3.1), double4(-5.3, double.PositiveInfinity, 4.7, 0.3)), 8, false);
            TestUtils.AreEqual(double.PositiveInfinity, distancesq(double4(1.3, double.NegativeInfinity, 5.7, 3.1), double4(-5.3, 4.3, 4.7, 0.3)), 8, false);
        }

        [TestCompiler]
        public static void epsilon_float()
        {
            float next = asfloat(asuint(1.0f) + 1);
            float computed_epsilon = next - 1.0f;
            TestUtils.AreEqual(computed_epsilon, EPSILON, 0.0f);
            TestUtils.IsTrue(Single.Epsilon != EPSILON);
        }

        [TestCompiler]
        public static void epsilon_double()
        {
            double next = asdouble(aslong(1.0) + 1);
            double computed_epsilon = next - 1.0;
            TestUtils.AreEqual(computed_epsilon, EPSILON_DBL, 0.0);
            TestUtils.IsTrue(Double.Epsilon != EPSILON_DBL);
        }

        [TestCompiler]
        public static void nan_float()
        {
            TestUtils.IsFalse(0.0f == NAN);
            TestUtils.IsFalse(INFINITY == NAN);
            TestUtils.IsFalse(isfinite(NAN));
            TestUtils.IsTrue(isnan(NAN));

            TestUtils.IsFalse(NAN < 0.0f);
            TestUtils.IsFalse(NAN <= 0.0f);
            TestUtils.IsFalse(NAN == 0.0f);
            TestUtils.IsFalse(NAN >= 0.0f);
            TestUtils.IsFalse(NAN > 0.0f);
            TestUtils.IsTrue(NAN != 0.0f);

            float nan1 = NAN;
            float nan2 = NAN;
            TestUtils.IsFalse(nan1 < nan2);
            TestUtils.IsFalse(nan1 <= nan2);
            TestUtils.IsFalse(nan1 == nan2);
            TestUtils.IsFalse(nan1 >= nan2);
            TestUtils.IsFalse(nan1 > nan2);
            TestUtils.IsTrue(nan1 != nan2);
        }

        [TestCompiler]
        public static void nan_double()
        {
            TestUtils.IsFalse(0.0 == NAN_DBL);
            TestUtils.IsFalse(INFINITY_DBL == NAN_DBL);
            TestUtils.IsFalse(isfinite(NAN_DBL));
            TestUtils.IsTrue(isnan(NAN_DBL));

            TestUtils.IsFalse(NAN_DBL < 0.0);
            TestUtils.IsFalse(NAN_DBL <= 0.0);
            TestUtils.IsFalse(NAN_DBL == 0.0);
            TestUtils.IsFalse(NAN_DBL >= 0.0);
            TestUtils.IsFalse(NAN_DBL > 0.0);
            TestUtils.IsTrue(NAN_DBL != 0.0);

            double nan1 = NAN_DBL;
            double nan2 = NAN_DBL;
            TestUtils.IsFalse(nan1 < nan2);
            TestUtils.IsFalse(nan1 <= nan2);
            TestUtils.IsFalse(nan1 == nan2);
            TestUtils.IsFalse(nan1 >= nan2);
            TestUtils.IsFalse(nan1 > nan2);
            TestUtils.IsTrue(nan1 != nan2);
        }

        [TestCompiler]
        public static void infinity_float()
        {
            TestUtils.IsTrue(-INFINITY != INFINITY);
            TestUtils.IsTrue(isinf(INFINITY));
            TestUtils.IsFalse(isfinite(INFINITY));
        }

        [TestCompiler]
        public static void infinity_double()
        {
            TestUtils.IsTrue(-INFINITY_DBL != INFINITY_DBL);
            TestUtils.IsTrue(isinf(INFINITY_DBL));
            TestUtils.IsFalse(isfinite(INFINITY_DBL));
        }

        [TestCompiler]
        public static void helper_axes()
        {
            TestUtils.AreEqual(float3(1.0f, 0.0f, 0.0f), right());
            TestUtils.AreEqual(float3(-1.0f, 0.0f, 0.0f), left());
            TestUtils.AreEqual(float3(0.0f, 1.0f, 0.0f), up());
            TestUtils.AreEqual(float3(0.0f, -1.0f, 0.0f), down());
            TestUtils.AreEqual(float3(0.0f, 0.0f, 1.0f), forward());
            TestUtils.AreEqual(float3(0.0f, 0.0f, -1.0f), back());
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void rcp_float_signed_zero()
        {
            TestUtils.AreEqual(float.NegativeInfinity, rcp(-0.0f));
            TestUtils.AreEqual(-0.0f, rcp(float.NegativeInfinity));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void rcp_float2_signed_zero()
        {
            TestUtils.AreEqual(float2(float.NegativeInfinity, -0.0f), rcp(float2(-0.0f, float.NegativeInfinity)));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void rcp_float3_signed_zero()
        {
            TestUtils.AreEqual(float3(float.NegativeInfinity, -0.0f, float.NegativeInfinity), rcp(float3(-0.0f, float.NegativeInfinity, -0.0f)));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void rcp_float4_signed_zero()
        {
            TestUtils.AreEqual(float4(float.NegativeInfinity, -0.0f, float.NegativeInfinity, -0.0f), rcp(float4(-0.0f, float.NegativeInfinity, -0.0f, float.NegativeInfinity)));
        }


        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void rcp_double_signed_zero()
        {
            TestUtils.AreEqual(double.NegativeInfinity, rcp(-0.0));
            TestUtils.AreEqual(-0.0, rcp(double.NegativeInfinity));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void rcp_double2_signed_zero()
        {
            TestUtils.AreEqual(double2(double.NegativeInfinity, -0.0), rcp(double2(-0.0, double.NegativeInfinity)));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void rcp_double3_signed_zero()
        {
            TestUtils.AreEqual(double3(double.NegativeInfinity, -0.0, double.NegativeInfinity), rcp(double3(-0.0, double.NegativeInfinity, -0.0)));
        }

        [TestCompiler]
        [WindowsOnly("Mono on linux ignores signed zero.")]
        public static void rcp_double4_signed_zero()
        {
            TestUtils.AreEqual(double4(double.NegativeInfinity, -0.0, double.NegativeInfinity, -0.0), rcp(double4(-0.0, double.NegativeInfinity, -0.0, double.NegativeInfinity)));
        }

        [TestCompiler]
        unsafe public static void compress_test()
        {
            int4 value = int4(0x12345678, 0x2468ACE0, 0x369BE147, 0x48C059D1);

            int ptrOffset = 4;
            int* dest = stackalloc int[16];
            int* ptr = dest + ptrOffset;

            for(int offset = -4; offset <= 4; offset++)
            {
                for (int m = 0; m < 16; m++)
                {
                    for (int i = 0; i < 16; i++)
                        dest[i] = 0;

                    bool4 mask = bool4((m & 1) != 0, (m & 2) != 0, (m & 4) != 0, (m & 8) != 0);
                    compress(ptr, offset, value, mask);

                    for(int i = 0; i < 16; i++)
                    {
                        int vectorIdx = i - (ptrOffset + offset);

                        int v = 0;
                        if (vectorIdx >= 0 && vectorIdx < 4)
                        {
                            for(int k = 0; k < 4; k++)
                            {
                                if (mask[k] && vectorIdx-- == 0)
                                    v = value[k];
                            }
                        }
                        TestUtils.AreEqual(v, dest[i]);
                    }
                }
            }
        }
    }
}
